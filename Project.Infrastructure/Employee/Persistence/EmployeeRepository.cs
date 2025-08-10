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

        public Task DeleteEmployeeAsync(int id)
        {
            _context.Employees
                .Where(e => e.EmployeeID == id)
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

        public async Task<List<Domain.ViewEmployeeData>> GetAllEmployeesAsync()
        {
            //var query = from e in _context.Employees
            //            //.Include(e => e.EmployeeTerritories)
            //            //.Include(e => e.Orders)
            //            join f in _context.FilePaths
            //            on e.EmployeeGuid equals f.EntityGuid into ef
            //            select new
            //            {
            //                Employee = e,
            //                File = ef.ToList()
            //            };
            //var lists = await query.ToListAsync();

            //foreach (var item in lists)
            //{
            //    item.Employee.EntityFiles = item.File;
            //}
            //return lists.Select(l => l.Employee).ToList();

            //return await _context.Employees
            //    .Include(e => e.Orders)
            //    .Include(e => e.EmployeeTerritories)
            //    .ToListAsync();
            /*
             
            select e.* ,
            (
	            select Path from FilePaths f
	            where f.EntityGuid = e.EmployeeGuid 
	            for json path
            ) as FilePaths
            from Employees e
            for json path
             
             */
            var emp = await _context.viewEmployeeDatas.ToListAsync();
            return emp;
        }

        public async Task<Domain.Employee> GetEmployeeByGuIdAsync(Guid? guid)
        {

            var query = from e in _context.Employees
                        .Include(e => e.EmployeeTerritories)
                        .Include(e => e.Orders)
                        join f in _context.FilePaths
                        on guid equals f.EntityGuid into ef
                        select new 
                        {
                            Employee = e,
                            File = ef.ToList()
                        };
            var list = await query.SingleOrDefaultAsync(e => e.Employee.EmployeeGuid == guid);
            list.Employee.EntityFiles = list.File;
            return list.Employee;

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
