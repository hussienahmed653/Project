using ErrorOr;
using MediatR;
using Project.Application.Authentication.Common;
using Project.Application.Authentication.Dtos;

namespace Project.Application.Authentication.Queries
{
    public record LoginQuerie(LoginRequest LoginRequest) : IRequest<ErrorOr<AuthReseult>>;
}
