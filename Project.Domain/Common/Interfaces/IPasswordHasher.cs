using ErrorOr;

namespace Project.Domain.Common.Interfaces
{
    public interface IPasswordHasher
    {
        public ErrorOr<string> HashPassword(string password);
        public bool IsCorrectPassword(string password, string hash);
    }
}
