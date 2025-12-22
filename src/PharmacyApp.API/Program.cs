using MediatR;
using PharmacyApp.Infrastructure;
using PharmacyApp.API.Requests.Order;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Register infrastructure (DbContext, repositories, unit of work)
builder.Services.AddInfrastructure(builder.Configuration);

// Register MediatR scanning all loaded assemblies so application handlers are found
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var mediator = app.Services.GetRequiredService<IMediator>();

// Minimal endpoints for order flow
app.MapPost("/api/orders/checkout", async (CheckoutRequest req) =>
{
    var id = await mediator.Send(new PharmacyApp.Application.CartManagement.Command.CheckoutCart.CheckoutCartCommand(
        req.CustomerId,
        req.ShippingAddress ?? string.Empty,
        req.BillingAddress ?? string.Empty,
        req.PaymentMethod ?? string.Empty
    ));

    return Results.Ok(new { OrderId = id });
});

app.MapPost("/api/orders/{orderId:guid}/cancel", async (Guid orderId, CancelRequest req) =>
{
    var dto = await mediator.Send(new PharmacyApp.Application.OrderManagement.Command.CancelOrder.CancelOrderCommand(orderId, req.CustomerId, req.Reason ?? string.Empty));
    return Results.Ok(dto);
});

app.MapPost("/api/admin/orders/{orderId:guid}/confirm", async (Guid orderId, AdminActionRequest req) =>
{
    var dto = await mediator.Send(new PharmacyApp.Application.OrderManagement.Command.ConfirmOrder.ConfirmOrderCommand(orderId, req.AdminId));
    return Results.Ok(dto);
});

app.MapPost("/api/admin/orders/{orderId:guid}/reject", async (Guid orderId,RejectOrderRequest req) =>
{
    var dto = await mediator.Send(new PharmacyApp.Application.OrderManagement.Command.RejectOrder.RejectOrderCommand(orderId, req.AdminId, req.Reason ?? string.Empty));
    return Results.Ok(dto);
});

app.Run();

// DTOs for minimal API requests
public record CheckoutRequest(Guid CustomerId, string? ShippingAddress, string? BillingAddress, string? PaymentMethod);
public record CancelRequest(Guid CustomerId, string? Reason);

