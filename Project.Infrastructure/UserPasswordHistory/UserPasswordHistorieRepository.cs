using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;

namespace Project.Infrastructure.UserPasswordHistory
{
    internal class UserPasswordHistorieRepository : IUserPasswordHistorieRepository
    {
        private readonly ApplicationDbContext _context;

        public UserPasswordHistorieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> ExistByPasswordHash(Guid userGuid)
        {
            return await _context.userPasswordHistories
                .Where(x => x.UserGuid == userGuid)
                .Select(x => x.OldPassword)
                .ToListAsync();
        }
    }
}
