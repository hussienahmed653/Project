using Dapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;
using Project.Infrastructure.DBContext;
using System.Data;

namespace Project.Infrastructure.Employee.Persistence
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _appcontext;
        private readonly DapperDbContext _dappercontext;

        public EmployeeRepository(ApplicationDbContext appcontext,
            DapperDbContext dappercontext)
        {
            _appcontext = appcontext;
            _dappercontext = dappercontext;
        }

        public async Task<int> AddEmployeeAsync(AddEmployeeDto employee)
        {
            //var sql = "insert into employees (EmployeeID, EmployeeGuid, LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Notes, ReportsTo) " +
            //          "values (@EmployeeID, @EmployeeGuid, @LastName, @FirstName, @Title, @TitleOfCourtesy, @BirthDate, @HireDate, @Address, @City, @Region, @PostalCode, @Country, @HomePhone, @Extension, @Notes, @ReportsTo)";
            using var connection = _dappercontext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<int>("InsertEmployee", employee, commandType: CommandType.StoredProcedure);

            //await _context.Employees.AddAsync(employee);
            //await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Guid guid)
        {
            var sql = "UPDATE Employees SET IsDeleted = 1 , DeletedOn = (select GETDATE()) WHERE  Employees.EmployeeGuid = @guid";
            using var connection = _dappercontext.CreateConnection();
            await connection.ExecuteAsync(sql, new { guid });
            //await _appcontext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(Guid guid)
        {
            var sql = "SELECT COUNT(1) FROM Employees WHERE EmployeeGuid = @guid AND IsDeleted = 0";
            using var connection = _dappercontext.CreateConnection();
            return await connection.ExecuteScalarAsync<bool>(sql, new { guid });
            //return _context.Employees
            //    .AsNoTracking()
            //    .Where(e => !e.IsDeleted)
            //    .AnyAsync(e => e.EmployeeGuid == guid);
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetAllTableViewEmployeesAsync()
        {
            var sql = "SELECT * FROM ViewEmployeeData";
            using var connection = _dappercontext.CreateConnection();
            var employee = await connection.QueryAsync<Domain.ViewClasses.ViewEmployeeData>(sql);
            return employee.ToList();
            //return await _appcontext.viewEmployeeDatas
            //    .Where(e => !e.IsDeleted)
            //    .ToListAsync();
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetTableViewEmployeeByGuIdAsync(Guid? guid)
        {
            var sql = "SELECT * FROM ViewEmployeeData where EmployeeGuid = @guid";
            using var connection = _dappercontext.CreateConnection();
            var employee = await connection.QueryAsync<Domain.ViewClasses.ViewEmployeeData>(sql, new { guid });
            return employee.ToList();
            //return await _appcontext.viewEmployeeDatas
            //    .Where(e => e.EmployeeGuid == guid && !e.IsDeleted)
            //    .ToListAsync();
        }

        public int GetMaxId()
        {
            var sql = "SELECT MAX(EmployeeID) FROM Employees";
            using var connection = _dappercontext.CreateConnection();
            return connection.QueryFirstOrDefault<int>(sql);
            //return _context.Employees.Any() ? _context.Employees.Max(e => e.EmployeeID) : 0;
        }

        public Task UpdateEmployeeAsync(Domain.Employee employee)
        {
            _appcontext.Update(employee);
            _appcontext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Domain.Employee> GetTableEmployeesAsync(Guid guid)
        {
            var sql = "SELECT * FROM Employees WHERE EmployeeGuid = @guid AND IsDeleted = 0";
            using var connection = _dappercontext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Domain.Employee>(sql, new { guid });
            //return await _context.Employees
            //    .Include(e => e.EmployeeTerritories)
            //    .SingleOrDefaultAsync(e => e.EmployeeGuid == guid);
        }
    }
}
