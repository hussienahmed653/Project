using Dapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Domain.Authentication;
using Project.Infrastructure.DBContext;
using System.Data;
using System.Net.Http.Headers;

namespace Project.Infrastructure.Users.Persistence
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(ApplicationDbContext context,
                              IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(User user)
        {
            return await _unitOfWork.connection.QueryFirstOrDefaultAsync<bool>("Register", user, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
            //await _context.Users.AddAsync(user);
            //await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistByEmail(string Email)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == Email);
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Email == Email);

        }

        public async Task<bool> Update(string email, int Role)
        {
            return await _unitOfWork.connection.QueryFirstOrDefaultAsync<bool>("UpdateUserRole", new { email, Role }, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
            //await _context.SaveChangesAsync();
        }
    }
}
