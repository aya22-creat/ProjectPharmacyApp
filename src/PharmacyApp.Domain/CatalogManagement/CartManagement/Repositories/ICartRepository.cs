using System;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common.Repositories;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories
{
    public interface ICartRepository : IRepository<Entities.Cart>
    {
        Task<Entities.Cart?> GetActiveCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
    }
}
