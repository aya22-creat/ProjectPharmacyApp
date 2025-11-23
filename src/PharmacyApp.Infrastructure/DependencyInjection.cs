using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement;
using PharmacyApp.Domain.CatalogManagement.ProductManagement;
using PharmacyApp.Domain.CheckoutFunctionality;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories;
using PharmacyApp.Infrastructure.Common;
using PharmacyApp.Infrastructure.Data;
using PharmacyApp.Infrastructure.Repositories;
using PharmacyApp.Domain.CheckoutFunctionality.Repositories;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;

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
            services.AddScoped<ICheckOutRepository, CheckoutRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}