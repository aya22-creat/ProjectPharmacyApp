using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.Category.CategoryAggregate;
using PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.CartManagement.Entities;


namespace PharmacyApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly ILogger<ApplicationDbContext> _logger;
    public DbSet<CategoryAggregate> Categories { get; set; }
    public DbSet<ProductAggregate> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }     
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        ILogger<ApplicationDbContext> logger)
        : base(options)
    {
        _mediator = mediator;
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.Ignore<DomainEvent>();
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync(cancellationToken);
        return result;
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        var domainEntities = ChangeTracker
            .Entries<AggregateRoot<Guid>>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var events = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(e => e.Entity.ClearDomainEvents());

        foreach (var domainEvent in events)
            await _mediator.Publish(domainEvent, cancellationToken);
    }
}
