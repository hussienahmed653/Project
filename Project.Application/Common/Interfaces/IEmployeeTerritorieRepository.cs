namespace Project.Application.Common.Interfaces
{
    public interface IEmployeeTerritorieRepository
    {
        public Task<bool> ExistAsync(int terid, int empid);
        public Task AddTerritoryToEmployee(int empid, Domain.EmployeeTerritorie entity);
        public Task RemoveTerritoryFromEmployee(Domain.EmployeeTerritorie entity);
    }
}
