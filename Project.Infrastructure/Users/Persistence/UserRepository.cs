using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Domain.Authentication;
using Project.Infrastructure.DBContext;
using System.Net.Http.Headers;

namespace Project.Infrastructure.Users.Persistence
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
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

        public async Task Update(User user)
        {
            await _context.SaveChangesAsync();
        }
    }
}
