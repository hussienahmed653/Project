using ErrorOr;
using Project.Application.Common.MediatorInterfaces;
using Project.Domain.Authentication;

namespace Project.Application.Authentication.Command.UpdateUserRole
{
    public record UpdateUserRoleCommand(string email, Roles Roles) : IRequestRepository<ErrorOr<Updated>>;
}
