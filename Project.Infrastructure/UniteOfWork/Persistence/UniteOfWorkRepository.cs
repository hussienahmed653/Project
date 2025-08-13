using Microsoft.EntityFrameworkCore.Storage;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;
using System.Reflection.Metadata.Ecma335;

namespace Project.Infrastructure.UniteOfWork.Persistence
{
    internal class UniteOfWorkRepository : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UniteOfWorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<bool> IfProbIsEmpty(object obj)
        {
            return obj.GetType()
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .Any(p => p.GetValue(obj) is not null && p.GetValue(obj) == "");
        }

        public async Task RollbackAsync()
        {
            if(_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
