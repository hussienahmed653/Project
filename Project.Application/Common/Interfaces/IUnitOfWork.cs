namespace Project.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public Task BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollbackAsync();
        public Task<bool> IfProbIsEmpty(object obj);
    }
}
