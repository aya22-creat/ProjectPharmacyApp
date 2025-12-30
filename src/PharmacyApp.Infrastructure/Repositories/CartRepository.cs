using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Infrastructure.Persistence;

namespace PharmacyApp.Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Cart?> GetActiveCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CustomerId == customerId && c.State == CartStateEnum.Active, cancellationToken);
        }

        public async Task<bool> ExistsForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(c => c.CustomerId == customerId, cancellationToken);
        }

        public async Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);
        }
        public async Task DeleteCartAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
