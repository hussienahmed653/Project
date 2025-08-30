using ErrorOr;
using Project.Application.Authentication.Common;
using Project.Application.Authentication.Dtos;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Authentication.Command.Register
{
    public record RegisterCommand(RegisterRequest Register) : IRequestRepository<ErrorOr<AuthReseult>>;
}
