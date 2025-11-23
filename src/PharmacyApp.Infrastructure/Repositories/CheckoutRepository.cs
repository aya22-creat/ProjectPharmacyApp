using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CheckoutFunctionality.Entities;
using PharmacyApp.Domain.CheckoutFunctionality.Repositories;
using PharmacyApp.Infrastructure.Data;
using PharmacyApp.Common.Common.Repositories;

namespace PharmacyApp.Infrastructure.Repositories
{
    public class CheckoutRepository : GenericRepository<CheckoutAggregate>, ICheckOutRepository
    {
        public CheckoutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CheckoutAggregate?> GetActiveByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.Status == CheckoutStatus.Pending, cancellationToken);
        }

        public async Task<bool> ExistsByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(c => c.CartId == cartId, cancellationToken);
        }

        public async Task<IEnumerable<CheckoutAggregate>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.CustomerId == customerId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasActiveCheckoutAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(c => c.CustomerId == customerId && c.Status == CheckoutStatus.Pending, cancellationToken);
        }

        public async Task<IEnumerable<CheckoutAggregate>> GetPendingCheckoutAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.CustomerId == customerId && c.Status == CheckoutStatus.Pending)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<CheckoutAggregate>> GetCompletedCheckoutsAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.CustomerId == customerId && c.Status == CheckoutStatus.Completed)
                .OrderByDescending(c => c.CompletedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(CheckoutAggregate checkout, CancellationToken cancellationToken = default)
        {
            if (checkout == null) throw new ArgumentNullException(nameof(checkout));

            var exists = await _dbSet.AnyAsync(c => c.Id == checkout.Id, cancellationToken);
            if (!exists)
            {
                throw new InvalidOperationException($"Checkout with Id {checkout.Id} not found in database.");
            }

            _dbSet.Update(checkout);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
