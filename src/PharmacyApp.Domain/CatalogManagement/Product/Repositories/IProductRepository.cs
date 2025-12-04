using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.CatalogManagement.Product.Repositories
{
    public interface IProductRepository : IRepository<ProductAggregate>
    {
        ProductAggregate? GetById(Guid productId);

        // Async retrieval
        Task<ProductAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task UpdateAsync(ProductAggregate product, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

        Task RemoveAsync(ProductAggregate product, CancellationToken cancellationToken = default);

       /*** Task<ProductAggregate?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
        Task<bool> ExistsBySkuAsync(string sku, CancellationToken cancellationToken = default);v **/
    }
}
