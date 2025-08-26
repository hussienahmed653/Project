using Project.Domain.Authentication;

namespace Project.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistByEmail(string Email);
        Task Add(User user);
        Task Update(User user);
        Task<User> GetUserByEmail(string Email); 
    }
}
