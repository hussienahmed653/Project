using ErrorOr;
using MediatR;
using Project.Application.DTOs;

namespace Project.Application.Employee.Queries.GetEmployee
{
    public record GetEmployeeQueries(Guid? Guid) : IRequest<List<EmployeeResponseDto>>;
}
