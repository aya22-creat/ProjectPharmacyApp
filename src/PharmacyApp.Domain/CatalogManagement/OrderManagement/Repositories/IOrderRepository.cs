using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.OrderAggregate;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
        Task<bool> ExistsByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
        Task RemoveAsync(Order order, CancellationToken cancellationToken = default);
    }
}
