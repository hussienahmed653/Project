using BCrypt.Net;
using ErrorOr;
using Project.Domain.Common.Interfaces;
using System.Text.RegularExpressions;

namespace Project.Infrastructure.Authentication.PasswordHasher
{
    internal partial class PasswordHasher : IPasswordHasher
    {
        private static readonly Regex StrongPassword = StrongPasswordRegex();

        public ErrorOr<string> HashPassword(string password)
        {
            return StrongPassword.IsMatch(password) 
                ? BCrypt.Net.BCrypt.EnhancedHashPassword(password)
                : Error.Validation(description: "Password is too weak!");
        }

        public bool IsCorrectPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }


        [GeneratedRegex("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])\\S{8,}$")]
        private static partial Regex StrongPasswordRegex();
    }
}
