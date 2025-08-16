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
        public Task AddEmployeeAsync(Domain.Employee employee);
        public Task DeleteEmployeeAsync();
        public Task UpdateEmployeeAsync(Domain.Employee employee);
    }
}
