using Project.Application.DTOs;
using Project.Domain;
using Project.Domain.ViewClasses;

namespace Project.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<List<ViewEmployeeData>> GetTableViewEmployeeByGuIdAsync(Guid? guid);
        public int GetMaxId();
        public Task<bool> ExistAsync(Guid guid);
        public Task<List<ViewEmployeeData>> GetAllTableViewEmployeesAsync();
        public Task<Domain.Employee> GetTableEmployeesAsync(Guid guid);
        public Task<int> AddEmployeeAsync(AddEmployeeDto employee);
        public Task DeleteEmployeeAsync(Guid guid);
        public Task UpdateEmployeeAsync(Domain.Employee employee);
    }
}
