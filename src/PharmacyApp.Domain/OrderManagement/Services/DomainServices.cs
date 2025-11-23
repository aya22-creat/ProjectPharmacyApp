using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Services
{

    public class OrderManagementService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManagementService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> IsOrderNumberUniqueAsync(string orderNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
                throw new DomainException("Order number cannot be empty.");

            var exists = await _orderRepository.ExistsByOrderNumberAsync(orderNumber, cancellationToken);
            return !exists;
        }

    }
}