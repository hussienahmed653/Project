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

        public Task DeleteEmployeeAsync(Guid guid)
        {
            _context.Employees
                .Where(e => e.EmployeeGuid == guid)
                .AsNoTracking()
                .ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task<bool> ExistAsync(Guid guid)
        {
            return _context.Employees
                .AsNoTracking()
                .AnyAsync(e => e.EmployeeGuid == guid);
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetAllEmployeesAsync()
        {
            var emp = await _context.viewEmployeeDatas.ToListAsync();
            return emp;
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetEmployeeByGuIdAsync(Guid? guid)
        {

            //var query = from e in _context.Employees
            //            .Include(e => e.EmployeeTerritories)
            //            .Include(e => e.Orders)
            //            join f in _context.FilePaths
            //            on guid equals f.EntityGuid into ef
            //            select new 
            //            {
            //                Employee = e,
            //                File = ef.ToList()
            //            };
            //var list = await query.SingleOrDefaultAsync(e => e.Employee.EmployeeGuid == guid);
            //list.Employee.EntityFiles = list.File;
            //return list.Employee;

            return await _context.viewEmployeeDatas
                .Where(e => e.EmployeeGuid == guid)
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
    }
}
