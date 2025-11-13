using PharmacyApp.Domain.CatalogManagement.ProductManagement.AggregateRoots;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories;
using PharmacyApp.Infrastructure.Data;

namespace PharmacyApp.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductAggregate>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new Task<ProductAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(id, cancellationToken);
        }

        public Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_context.Set<ProductAggregate>().Any(p => p.Id == id));
        }

        public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_context.Set<ProductAggregate>().Any(p => p.Name == name));
        }

        public Task RemoveAsync(ProductAggregate product, CancellationToken cancellationToken = default)
        {
            _context.Set<ProductAggregate>().Remove(product);
            return Task.CompletedTask;
        }
    }
}
