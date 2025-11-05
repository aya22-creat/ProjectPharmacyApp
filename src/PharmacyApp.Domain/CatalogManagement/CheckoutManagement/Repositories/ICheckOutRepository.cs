using System;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities;

namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories
{
    public interface ICheckOutRepository : IRepository<CheckoutAggregate>
    {
        Task<bool> ExistsByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default);
    }
}
