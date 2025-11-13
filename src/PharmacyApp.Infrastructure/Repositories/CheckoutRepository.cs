using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories;
using PharmacyApp.Infrastructure.Data;
using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Common.Common;

namespace PharmacyApp.Infrastructure.Repositories
{
    public class CheckoutRepository : GenericRepository<CheckoutAggregate>, ICheckOutRepository
    {
        public CheckoutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CheckoutAggregate?> GetActiveByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(
                c => c.CustomerId == customerId && c.Status == CheckoutStatus.Pending, cancellationToken);
        }

        public async Task<bool> ExistsByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(c => c.CartId == cartId, cancellationToken);
        }

        public async Task<IEnumerable<CheckoutAggregate>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(c => c.CustomerId == customerId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasActiveCheckoutAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(
                c => c.CustomerId == customerId && c.Status == CheckoutStatus.Pending, cancellationToken);
        }

        public async Task<IEnumerable<CheckoutAggregate>> GetPendingCheckoutAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(c => c.CustomerId == customerId && c.Status == CheckoutStatus.Pending)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<CheckoutAggregate>> GetCompletedCheckoutsAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(c => c.CustomerId == customerId && c.Status == CheckoutStatus.Completed)
                .OrderByDescending(c => c.CompletedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
