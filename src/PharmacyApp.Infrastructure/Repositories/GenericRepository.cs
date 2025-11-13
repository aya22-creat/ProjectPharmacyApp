using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.CategoryAggregate;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Repositories;
using PharmacyApp.Infrastructure.Data;
using PharmacyApp.Common.Common.Repositories;
using PharmacyApp.Common.Common;


namespace PharmacyApp.Infrastructure.Repositories
{

    public class GenericRepository<T> : PharmacyApp.Common.Common.Repositories.IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Add(entity);
            return Task.CompletedTask;
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken) != null;
        }
    }
}
