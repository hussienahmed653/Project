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
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeRepository(ApplicationDbContext appcontext,
            IUnitOfWork unitOfWork)
        {
            _appcontext = appcontext;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddEmployeeAsync(AddEmployeeDto employee)
        {
            return await _unitOfWork.connection.QueryFirstOrDefaultAsync<int>("InsertEmployee", employee, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);

        }


        public async Task<int> DeleteEmployeeAsync(Guid guid)
        {
            return await _unitOfWork.connection.QuerySingleOrDefaultAsync<int>("DeleteEmployee", new { guid }, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> ExistAsync(Guid guid)
        {
            var sql = "SELECT COUNT(1) FROM Employees WHERE EmployeeGuid = @guid AND IsDeleted = 0";
            return await _unitOfWork.connection.ExecuteScalarAsync<bool>(sql, new { guid }, _unitOfWork.transaction);
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetAllTableViewEmployeesAsync()
        {
            var sql = "SELECT * FROM ViewEmployeeData";
            var employee = await _unitOfWork.connection.QueryAsync<Domain.ViewClasses.ViewEmployeeData>(sql, _unitOfWork.transaction);
            return employee.ToList();
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetTableViewEmployeeByGuIdAsync(Guid? guid)
        {
            var sql = "SELECT * FROM ViewEmployeeData where EmployeeGuid = @guid";
            var employee = await _unitOfWork.connection.QueryAsync<Domain.ViewClasses.ViewEmployeeData>(sql, new { guid }, _unitOfWork.transaction);
            return employee.ToList();
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
            return await _unitOfWork.connection.QuerySingleOrDefaultAsync<Domain.Employee>(sql, new { guid }, _unitOfWork.transaction);
        }
    }
}
