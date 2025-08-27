using Project.Domain.TrigerUpdateUserTable;

namespace Project.Application.Common.Interfaces
{
    public interface IUserPasswordHistorieRepository
    {
        Task<List<string>> ExistByPasswordHash(Guid userGuid);
    }
}
