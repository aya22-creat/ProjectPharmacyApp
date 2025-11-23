using MediatR;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.Common;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.CategoryAggregate;
using PharmacyApp.Domain.CheckoutFunctionality.Entities;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;


namespace PharmacyApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryAggregate> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CheckoutAggregate> Checkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Ignore<PharmacyApp.Common.Common.DomainEvent.DomainEvent>();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchDomainEventsAsync(cancellationToken);

            return result;
        }

        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
        {
            {
                var domainEntities = ChangeTracker
                    .Entries<AggregateRoot<Guid>>()
                    .Where(x => x.Entity.DomainEvents.Any())
                    .ToList();

                var domainEvents = domainEntities
                    .SelectMany(x => x.Entity.DomainEvents)
                    .ToList();

                foreach (var entity in domainEntities)
                {
                    entity.Entity.ClearDomainEvents();
                }

                foreach (var domainEvent in domainEvents)
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }

            }
        }
    }
}

