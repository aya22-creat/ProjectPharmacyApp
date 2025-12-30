
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Common.Common.Repositories;

namespace PharmacyApp.Domain.CartManagement.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetActiveCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<bool> ExistsForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task DeleteCartAsync(Cart cart, CancellationToken cancellationToken = default);

    }
}
