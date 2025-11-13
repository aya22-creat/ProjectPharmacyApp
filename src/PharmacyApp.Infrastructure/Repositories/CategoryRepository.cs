using PharmacyApp.Domain.CatalogManagement.CategoryManagement.CategoryAggregate;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Repositories;
using PharmacyApp.Infrastructure.Data;

namespace PharmacyApp.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryAggregate>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<CategoryAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(id, cancellationToken);
        }

        public override Task<IEnumerable<CategoryAggregate>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return base.GetAllAsync(cancellationToken);
        }

        public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_context.Set<CategoryAggregate>().Any(c => c.Name == name));
        }

        public override Task AddAsync(CategoryAggregate category, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(category, cancellationToken);
        }

        public void Delete(CategoryAggregate category)
        {
            base.Remove(category);
        }
    }
}
