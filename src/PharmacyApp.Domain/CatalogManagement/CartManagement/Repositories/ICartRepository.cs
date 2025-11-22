
using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;
using PharmacyApp.Common.Common.Repositories;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories
{
    public interface ICartRepository : IRepository<Entities.Cart>
    {
        Task<Entities.Cart?> GetActiveCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<bool> ExistsForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    }
}
