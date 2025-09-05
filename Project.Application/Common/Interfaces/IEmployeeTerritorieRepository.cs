namespace Project.Application.Common.Interfaces
{
    public interface IEmployeeTerritorieRepository
    {
        public Task<int> AddTerritoryToEmployee(Guid empguid, int terid);
        public Task<int> RemoveTerritoryFromEmployee(Guid empguid, int terid);
    }
}
