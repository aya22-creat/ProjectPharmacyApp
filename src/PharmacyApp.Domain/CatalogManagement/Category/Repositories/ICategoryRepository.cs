using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common;
using CategoryAgg = PharmacyApp.Domain.CatalogManagement.CategoryManagement.CategoryAggregate.CategoryAggregate;



namespace PharmacyApp.Domain.CatalogManagement.CategoryManagement.Repositories
{



    public interface IRepository<T> { }
    // any generic methods for repository can be defined here

    public interface ICategoryRepository : IRepository<CategoryAgg>
    {
        Task<CategoryAgg?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryAgg>> GetAllAsync(CancellationToken cancellationToken = default);

        //check if category with the same name exists
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task AddAsync(CategoryAgg category, CancellationToken cancellationToken = default);
        void Delete(CategoryAgg category);
    }
}
