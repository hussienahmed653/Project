using Project.Application.Common.Interfaces;
using Project.Application.Common.Models;
using System.Security.Claims;

namespace Project.Api.Services
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
    {
        public CurrentUser GetCurrentUser()
        {
            var userId = GetClaimValues("UserId")
                .Select(Guid.Parse)
                .First();

            var email = GetClaimValues(ClaimTypes.Email)
                .First();

            var role = GetClaimValues(ClaimTypes.Role)
                .First();

            return new CurrentUser(userId, email, role);
        }

        private IReadOnlyList<string> GetClaimValues(string claimType)
        {
            return _httpContextAccessor.HttpContext.User.Claims
                .Where(c => c.Type == claimType)
                .Select(c => c.Value)
                .ToList();
        }
    }
}
