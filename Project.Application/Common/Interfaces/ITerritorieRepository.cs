namespace Project.Application.Common.Interfaces
{
    public interface ITerritorieRepository
    {
        Task<bool> ExistAsync(int terid);
    }
}
