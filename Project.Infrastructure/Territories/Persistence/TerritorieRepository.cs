using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;

namespace Project.Infrastructure.Territories.Persistence
{
    internal class TerritorieRepository : ITerritorieRepository
    {
        private readonly ApplicationDbContext _context;

        public TerritorieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistAsync(int terid)
        {
            return await _context.Territorie
                .AnyAsync(t => t.TerritoryID == terid);
        }
    }
}
