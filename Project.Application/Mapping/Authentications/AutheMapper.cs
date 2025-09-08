using Project.Application.Authentication.Common;
using Project.Application.DTOs;
using Project.Domain.Authentication;

namespace Project.Application.Mapping.Authentications
{
    public static class AutheMapper
    {
        public static User MapToUser(this RegisterRequest register)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                CreatedOn = DateTime.UtcNow,
            };
        }

        public static AuthReseult MapToAuthResult(this User user)
        {
            return new AuthReseult
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }
    }
}
