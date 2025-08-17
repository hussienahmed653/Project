using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;

namespace Project.Infrastructure.Supplier.Persistence
{
    internal class SuppliersRepository : ISuppliersRepository
    {
        private readonly ApplicationDbContext _context;

        public SuppliersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> FindAsync(int? supplierId)
        {
            return await _context.Supplier
                .AsNoTracking()
                .AnyAsync(s => s.SupplierID == supplierId);
        }
    }
}
