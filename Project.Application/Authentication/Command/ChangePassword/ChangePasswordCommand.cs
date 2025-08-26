using ErrorOr;
using MediatR;
using Project.Application.Authentication.Dtos;

namespace Project.Application.Authentication.Command.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordRequest ChangePasswordRequest) : IRequest<ErrorOr<Updated>>;
}
