using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.AggregateRoots;

namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories
{
    public interface IProductRepository : IRepository<ProductAggregate>
    {
        new Task<ProductAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task RemoveAsync(ProductAggregate product, CancellationToken cancellationToken = default);
    }
}

// anthor way 
// public interface IProductRepository : IRepository<ProductId, ProductAggregate>
// {
//     Task<ProductAggregate?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
//     Task<bool> ExistsBySkuAsync(string sku, CancellationToken cancellationToken = default);
//     Task RemoveAsync(ProductAggregate product, CancellationToken cancellationToken = default);
// }
