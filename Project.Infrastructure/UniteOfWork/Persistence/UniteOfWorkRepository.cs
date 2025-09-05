using Microsoft.Data.SqlClient;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;
using System.Data;

namespace Project.Infrastructure.UniteOfWork.Persistence
{
    internal class UniteOfWorkRepository : IUnitOfWork
    {
        private readonly DapperDbContext _context;
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;
        public IDbConnection connection => _connection;
        public IDbTransaction transaction => _transaction;

        public UniteOfWorkRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            //if (_transaction == null)
            //    _transaction = await _context.Database.BeginTransactionAsync();
            if(_transaction == null)
            {
                _connection = _context.CreateConnection();
                await (_connection as SqlConnection)!.OpenAsync();
                _transaction = _connection.BeginTransaction();
            }
        }

        public Task CommitAsync()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
            return Task.CompletedTask;
        }

        public Task RollbackAsync()
        {
            if(_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
            return Task.CompletedTask;
        }
    }
}
