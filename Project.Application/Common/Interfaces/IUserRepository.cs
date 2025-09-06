using Project.Domain.Authentication;

namespace Project.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistByEmail(string Email);
        Task<bool> Add(User user);
        Task<bool> Update(string email, int Role);
        Task<User> GetUserByEmail(string Email); 
    }
}
