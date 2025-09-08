using ErrorOr;
using Project.Application.Authentication.Common;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;

namespace Project.Application.Authentication.Queries
{
    public record LoginQuerie(LoginRequest LoginRequest) : IRequestRepository<ErrorOr<AuthReseult>>;
}
