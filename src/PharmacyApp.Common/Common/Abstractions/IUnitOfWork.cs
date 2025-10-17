using PharmacyApp.Common.Common.models;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Common.Abstractions
{
   public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}

}