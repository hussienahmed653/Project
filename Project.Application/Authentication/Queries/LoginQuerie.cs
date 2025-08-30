using ErrorOr;
using Project.Application.Authentication.Common;
using Project.Application.Authentication.Dtos;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Authentication.Queries
{
    public record LoginQuerie(LoginRequest LoginRequest) : IRequestRepository<ErrorOr<AuthReseult>>;
}
