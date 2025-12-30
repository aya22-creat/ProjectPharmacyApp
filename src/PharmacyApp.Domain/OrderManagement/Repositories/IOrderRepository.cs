using PharmacyApp.Domain.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.OrderManagement.Enums;
using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Domain.CartManagement.Entities;


namespace PharmacyApp.Domain.OrderManagement.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
        Task<bool> ExistsByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
        Task RemoveAsync(Order order, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersByStateAsync(OrderStateEnum state, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
        Task<int> GetOrdersCountByStateAsync(OrderStateEnum state, CancellationToken cancellationToken = default);
       Task<Order> CreateOrderFromCartAsync(Guid cartId,IEnumerable<CartItem> items, Guid customerId,CancellationToken cancellationToken
);

    }
}
