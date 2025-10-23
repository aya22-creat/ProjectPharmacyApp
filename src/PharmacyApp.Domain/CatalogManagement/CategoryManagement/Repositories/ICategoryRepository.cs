using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common;



namespace PharmacyApp.Domain.CatalogManagement.CategoryManagement.Repositories
{
    public class CategoryAggregate { }

    public interface IRepository<T> { }

    public interface ICategoryRepository : IRepository<CategoryAggregate>
    {
        Task<CategoryAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryAggregate>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
