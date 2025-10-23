using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.AggregateRoots;
namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories
{
  public interface IRepository<T> { }

  public interface IProductRepository : IRepository<ProductAggregate>
  {
    Task<ProductAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductAggregate>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
  }
}
