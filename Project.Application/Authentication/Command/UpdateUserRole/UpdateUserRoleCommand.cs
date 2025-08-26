using ErrorOr;
using MediatR;
using Project.Domain.Authentication;

namespace Project.Application.Authentication.Command.UpdateUserRole
{
    public record UpdateUserRoleCommand(string email, Roles Roles) : IRequest<ErrorOr<Updated>>;
}
