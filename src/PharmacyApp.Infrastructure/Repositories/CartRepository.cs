using System.Linq;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories;
using PharmacyApp.Infrastructure.Data;

namespace PharmacyApp.Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Cart?> GetActiveCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CustomerId == customerId && c.State == CartState.Active, cancellationToken);
        }

        public async Task<bool> ExistsForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(c => c.CustomerId == customerId, cancellationToken);
        }

        public async Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);
        }
    }
}
