using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyApp.Domain.CatalogManagement.Category.Repositories;
using PharmacyApp.Domain.CatalogManagement.Product.Repositories;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using PharmacyApp.Domain.CartManagement.Services;
using PharmacyApp.Infrastructure.Common;
using PharmacyApp.Infrastructure.MessageQueue.Configuration;
using PharmacyApp.Infrastructure.Persistence;
using PharmacyApp.Infrastructure.Repositories;
using PharmacyApp.Infrastructure.Services;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.OrderManagement.Repositories;
using PharmacyApp.Application.Common;

namespace PharmacyApp.Infrastructure
{
    public static class DependencyInjection
    {
    
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<ICartCalculationService, CartCalculationService>();

            services.AddMessageQueue(configuration);


            return services;
        }
    }
}
