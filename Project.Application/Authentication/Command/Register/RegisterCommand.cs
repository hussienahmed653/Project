using ErrorOr;
using Project.Application.Authentication.Common;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;

namespace Project.Application.Authentication.Command.Register
{
    public record RegisterCommand(RegisterRequest Register) : IRequestRepository<ErrorOr<AuthReseult>>;
}
