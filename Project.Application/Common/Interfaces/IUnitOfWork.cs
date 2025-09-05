using System.Data;

namespace Project.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public Task BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollbackAsync();
        public IDbConnection connection { get; }
            
        public IDbTransaction transaction { get; }
    }
}
