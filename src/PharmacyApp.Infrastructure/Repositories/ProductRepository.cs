using PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots;
using PharmacyApp.Domain.CatalogManagement.Product.Repositories;
using PharmacyApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductAggregate>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ProductAggregate? GetById(Guid productId)
        {
            return _context.Set<ProductAggregate>().FirstOrDefault(p => p.Id == productId);
        }

        public async new Task<ProductAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ProductAggregate>()
                                 .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(ProductAggregate product, CancellationToken cancellationToken = default)
        {
            _context.Set<ProductAggregate>().Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ProductAggregate>().AnyAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ProductAggregate>().AnyAsync(p => p.Name == name, cancellationToken);
        }

        public async Task RemoveAsync(ProductAggregate product, CancellationToken cancellationToken = default)
        {
            _context.Set<ProductAggregate>().Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
