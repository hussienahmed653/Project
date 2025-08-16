using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;

namespace Project.Infrastructure.Employee.Persistence
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployeeAsync(Domain.Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistAsync(Guid guid)
        {
            return _context.Employees
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .AnyAsync(e => e.EmployeeGuid == guid);
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetAllTableViewEmployeesAsync()
        {
            return await _context.viewEmployeeDatas
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetTableViewEmployeeByGuIdAsync(Guid? guid)
        {
            return await _context.viewEmployeeDatas
                .Where(e => e.EmployeeGuid == guid && !e.IsDeleted)
                .ToListAsync();
        }

        public int GetMaxId()
        {
            return _context.Employees.Any() ? _context.Employees.Max(e => e.EmployeeID) : 0;
        }

        public Task UpdateEmployeeAsync(Domain.Employee employee)
        {
            _context.Update(employee);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Domain.Employee> GetTableEmployeesAsync(Guid guid)
        {
            return await _context.Employees
                .Include(e => e.EmployeeTerritories)
                .SingleOrDefaultAsync(e => e.EmployeeGuid == guid);
        }
    }
}
