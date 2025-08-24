using ErrorOr;
using MediatR;
using Project.Application.Authentication.Common;
using Project.Application.Authentication.Dtos;

namespace Project.Application.Authentication.Command
{
    public record RegisterCommand(RegisterRequest Register) : IRequest<ErrorOr<AuthReseult>>;
}
