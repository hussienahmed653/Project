using ErrorOr;
using Project.Application.Authentication.Dtos;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Authentication.Command.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordRequest ChangePasswordRequest) : IRequestRepository<ErrorOr<Updated>>;
}
