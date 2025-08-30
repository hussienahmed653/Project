using ErrorOr;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;

namespace Project.Application.Employee.Queries.GetEmployee
{
    public record GetEmployeeQueries(Guid? Guid) : IRequestRepository<ErrorOr<List<EmployeeResponseDto>>>;
}
