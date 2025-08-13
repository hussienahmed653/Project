using Project.Domain.ViewClasses;

namespace Project.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<List<ViewEmployeeData>> GetEmployeeByGuIdAsync(Guid? guid);
        public int GetMaxId();
        public Task<bool> ExistAsync(Guid guid);
        public Task<List<ViewEmployeeData>> GetAllEmployeesAsync();
        public Task AddEmployeeAsync(Domain.Employee employee);
        public Task DeleteEmployeeAsync(Guid guid);
        public Task UpdateEmployeeAsync(Domain.Employee employee);
    }
}
