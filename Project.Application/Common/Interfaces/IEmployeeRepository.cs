using Project.Application.DTOs;
using Project.Domain;
using Project.Domain.ViewClasses;
using System.Data;

namespace Project.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<List<ViewEmployeeData>> GetTableViewEmployeeByGuIdAsync(Guid? guid);
        public Task<bool> ExistAsync(Guid guid);
        public Task<List<ViewEmployeeData>> GetAllTableViewEmployeesAsync();
        public Task<Domain.Employee> GetTableEmployeesAsync(Guid guid);
        public Task<int> AddEmployeeAsync(AddEmployeeDto employee);
        public Task<int> DeleteEmployeeAsync(Guid guid);
        public Task UpdateEmployeeAsync(Domain.Employee employee);
    }
}
