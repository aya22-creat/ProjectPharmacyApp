using System;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.CatalogManagement.Product.Services
{
    public interface IStockService
    {
        Task ReserveStockAsync(Guid productId, int quantity, CancellationToken cancellationToken);
        Task ReleaseStockAsync(Guid productId, int quantity, CancellationToken cancellationToken);
    }
}

