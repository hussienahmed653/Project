namespace Project.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<Domain.Employee> GetEmployeeByGuIdAsync(Guid? guid);
        public int GetMaxId();
        public Task<bool> ExistAsync(Guid guid);
        public Task<List<Domain.ViewEmployeeData>> GetAllEmployeesAsync();
        public Task AddEmployeeAsync(Domain.Employee employee);
        public Task DeleteEmployeeAsync(int id);
        public Task UpdateEmployeeAsync(Domain.Employee employee);
    }
}
