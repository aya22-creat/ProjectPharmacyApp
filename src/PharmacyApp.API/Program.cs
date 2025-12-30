using MediatR;
using PharmacyApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using PharmacyApp.Domain.CartManagement.Services;
using PharmacyApp.Application.Common;
using PharmacyApp.Infrastructure;
using PharmacyApp.Application;
using PharmacyApp.Domain.CatalogManagement.Category.CategoryAggregate;
using PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots;
using PharmacyApp.Domain.CatalogManagement.Product.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Category.ValueObjects;
using PharmacyApp.Common.Common.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("PharmacyApp.Infrastructure") 
    )
);

// ======= Controllers + JSON Options =======
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

    builder.Services.AddScoped<ICartCalculationService, CartCalculationService>();




// ======= Swagger / OpenAPI =======
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pharmacy Sample API",
        Version = "v1",
        Description = "Sample API for Pharmacy Management System (Cosmetics & Medicine)",
        Contact = new OpenApiContact
        {
            Name = "Pharmacy Team",
            Email = "support@pharmacy.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});




// ======= Layers / DI =======
builder.Services.AddApplication(); // Application Layer
builder.Services.AddInfrastructure(builder.Configuration); // Infrastructure Layer

// MediatR (CQRS)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    options.AddPolicy("Production", policy =>
        policy.WithOrigins() 
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

await EnsureDatabaseAsync(app);

// ======= Middleware =======
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharmacy Sample API V1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// CORS
app.UseCors(app.Environment.IsDevelopment() ? "AllowAll" : "Production");

// Authorization (Optional)
app.UseAuthorization();

// Health Check Endpoint
app.MapHealthChecks("/health");

// Map Controllers
app.MapControllers();

app.Run();

// ======= Helper Methods =======
static async Task EnsureDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await context.Database.MigrateAsync();
    await SeedDataAsync(context);
}

static async Task SeedDataAsync(ApplicationDbContext context)
{
    if (!context.Categories.Any())
    {
        // Categories
        var cosmetics = CategoryAggregate.Create("Cosmetics", "Beauty and cosmetic products");
        var medicine = CategoryAggregate.Create("Medicine", "Pharmaceutical products");

        context.Categories.AddRange(cosmetics, medicine);
        await context.SaveChangesAsync();

        // Products
        var lipstick = ProductAggregate.Create(
            "Red Lipstick",
            new ProductDescription("A vibrant red lipstick for everyday use."),
            Money.Create(150m, "EGP"),
            100,
            true,
            CategoryId.Create(cosmetics.Id)
        );

        var cream = ProductAggregate.Create(
            "Moisturizing Cream",
            new ProductDescription("Hydrating cream for dry skin."),
            Money.Create(200m, "EGP"),
            50,
            true,
            CategoryId.Create(cosmetics.Id)
        );

        var painkiller = ProductAggregate.Create(
            "Paracetamol 500mg",
            new ProductDescription("Pain relief medication."),
            Money.Create(25m, "EGP"),
            200,
            false,
            CategoryId.Create(medicine.Id)
        );

        var vitaminC = ProductAggregate.Create(
            "Vitamin C 500mg",
            new ProductDescription("Vitamin C supplement for immunity."),
            Money.Create(40m, "EGP"),
            150,
            false,
            CategoryId.Create(medicine.Id)
        );

        context.Products.AddRange(lipstick, cream, painkiller, vitaminC);
        await context.SaveChangesAsync();
    }
}
