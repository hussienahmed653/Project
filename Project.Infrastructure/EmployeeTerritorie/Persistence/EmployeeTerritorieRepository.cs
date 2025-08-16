using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;

namespace Project.Infrastructure.EmployeeTerritorie.Persistence
{
    internal class EmployeeTerritorieRepository : IEmployeeTerritorieRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTerritorieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTerritoryToEmployee(int empid, Domain.EmployeeTerritorie entity)
        {
            var employeeterritory = await _context.Employees
                .Include(e => e.EmployeeTerritories)
                .FirstOrDefaultAsync(e => e.EmployeeID == empid);

            employeeterritory.EmployeeTerritories.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(int terid, int empid)
        {
            //return await _context.Employees
            //    .Where(e => e.EmployeeTerritories.Any(et => et.TerritoryID != terid && et.EmployeeID != empid) 
            //    && e.EmployeeTerritories.Any(t => t.territorie.TerritoryID == terid)).AnyAsync();

            return await _context.EmployeeTerritories
                .AnyAsync(et => et.TerritoryID == terid && et.EmployeeID == empid);

        }

        public async Task RemoveTerritoryFromEmployee(Domain.EmployeeTerritorie entity)
        {
            _context.EmployeeTerritories.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
