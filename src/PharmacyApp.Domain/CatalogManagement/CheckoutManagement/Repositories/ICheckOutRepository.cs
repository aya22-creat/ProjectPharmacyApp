using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities;

namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories
{
    public interface ICheckOutRepository : IRepository<CheckoutAggregate>
    {
        Task<bool> ExistsByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default);
        Task<CheckoutAggregate?> GetActiveByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<CheckoutAggregate>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<bool> HasActiveCheckoutAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<CheckoutAggregate>> GetPendingCheckoutAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<CheckoutAggregate>> GetCompletedCheckoutsAsync(Guid customerId, CancellationToken cancellationToken = default);
    }
}
