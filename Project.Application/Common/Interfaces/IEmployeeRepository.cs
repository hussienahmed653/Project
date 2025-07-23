using Project.Application.DTOs;
using Project.Domain;

namespace Project.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<Domain.Employee> GetEmployeeByGuIdAsync(Guid guid);
        public int GetMaxId();
        public Task<bool> ExistAsync(int id);
        public Task<List<Domain.Employee>> GetAllEmployeesAsync();
        public Task AddEmployeeAsync(Domain.Employee employee);
        public Task DeleteEmployeeAsync(int id);
        public Task UpdateEmployeeAsync(Domain.Employee employee);
    }
}
