using ErrorOr;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;

namespace Project.Application.Authentication.Command.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordRequest ChangePasswordRequest) : IRequestRepository<ErrorOr<Updated>>;
}
