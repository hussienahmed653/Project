using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;

namespace Project.Infrastructure.Categories.Persistence
{
    internal class CatecoryRepository : ICatecoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CatecoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> FindAsync(int? categoryId)
        {
            return await _context.Categories
                .AsNoTracking()
                .AnyAsync(c => c.CategoryID == categoryId);
        }
    }
}
