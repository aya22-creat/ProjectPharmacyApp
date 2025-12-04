using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common;

// alias points to the namespace (folder), not class
using CategoryAgg = PharmacyApp.Domain.CatalogManagement.Category.CategoryAggregate.CategoryAggregate;

namespace PharmacyApp.Domain.CatalogManagement.Category.Repositories;

    public interface IRepository<T> { }

    public interface ICategoryRepository : IRepository<CategoryAgg>
    {
        Task<CategoryAgg?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryAgg>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

        Task AddAsync(CategoryAgg category, CancellationToken cancellationToken = default);

        void Delete(CategoryAgg category);
    }

