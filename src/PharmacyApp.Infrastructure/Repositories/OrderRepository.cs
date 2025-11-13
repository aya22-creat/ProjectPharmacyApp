using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;
using PharmacyApp.Infrastructure.Data;
using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Common.Common;

namespace PharmacyApp.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber, cancellationToken);
        }

        public async Task<bool> ExistsByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(o => o.OrderNumber == orderNumber, cancellationToken);
        }

        public async Task RemoveAsync(Order order, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(order);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetPendingOrdersAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                .Where(o => o.Status == OrderStatus.Pending)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetCompletedOrdersAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                .Where(o => o.Status == OrderStatus.Completed)
                .OrderByDescending(o => o.CompletedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetCancelledOrdersAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                .Where(o => o.Status == OrderStatus.Cancelled)
                .OrderByDescending(o => o.CancelledAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<decimal> GetTotalRevenueAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(o => o.Status == OrderStatus.Completed);

            if (startDate.HasValue)
                query = query.Where(o => o.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(o => o.CreatedAt <= endDate.Value);

            return await query.SumAsync(o => o.TotalAmount.Amount, cancellationToken);
        }

        public async Task<int> GetOrdersCountByStatusAsync(
            OrderStatus status,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(o => o.Status == status, cancellationToken);
        }
    }
}
