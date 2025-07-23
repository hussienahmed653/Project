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

        public Task AddEmployeeAsync(Domain.Employee employee)
        {
            _context.Employees.AddAsync(employee);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteEmployeeAsync(int id)
        {
            _context.Employees
                .Where(e => e.EmployeeID == id)
                .AsNoTracking()
                .ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task<bool> ExistAsync(int id)
        {
            return _context.Employees
                .AsNoTracking()
                .AnyAsync(e => e.EmployeeID == id);
        }

        public Task<List<Domain.Employee>> GetAllEmployeesAsync()
        {
            return _context.Employees
                .ToListAsync();
        }

        public Task<Domain.Employee> GetEmployeeByGuIdAsync(Guid guid)
        {
            return _context.Employees
                .SingleOrDefaultAsync(e => e.EmployeeGuid ==guid);
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
    }
}
