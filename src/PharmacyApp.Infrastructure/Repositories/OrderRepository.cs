using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.OrderManagement.Enums;
using PharmacyApp.Domain.OrderManagement.Repositories;
using PharmacyApp.Infrastructure.Persistence;
using PharmacyApp.Domain.CartManagement.Entities;


namespace PharmacyApp.Infrastructure.Repositories;

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(o => o.Items)
                               .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(o => o.Items)
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
            return await _dbSet.Include(o => o.Items)
                               .Where(o => o.CustomerId == customerId)
                               .OrderByDescending(o => o.CreatedAt)
                               .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStateAsync(OrderStateEnum state, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(o => o.Items)
                               .Where(o => o.State.Value == state.Value)
                               .OrderByDescending(o => o.CreatedAt)
                               .ToListAsync(cancellationToken);
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(o => o.State.Value == OrderStateEnum.Completed.Value);

            if (startDate.HasValue)
                query = query.Where(o => o.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(o => o.CreatedAt <= endDate.Value);

            return await query.Select(o => o.TotalAmount.Amount)
                              .SumAsync(cancellationToken);
        }

        public async Task<int> GetOrdersCountByStateAsync(OrderStateEnum state, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(o => o.State.Value == state.Value, cancellationToken);
        }

        public async Task<Order> CreateOrderFromCartAsync(Guid cartId, IEnumerable<CartItem> items, Guid customerId, CancellationToken cancellationToken)
        {
            var order = new Order(customerId,
    shippingAddress: "123 Main St",
    billingAddress: "123 Main St",
    paymentMethod: "CreditCard");

            await _dbSet.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return order;
    }
}
