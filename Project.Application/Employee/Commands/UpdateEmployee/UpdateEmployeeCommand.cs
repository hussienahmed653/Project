using ErrorOr;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;

namespace Project.Application.Employee.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(UpdateEmployeeDto UpdateEmployeeDTO) : IRequestRepository<ErrorOr<EmployeeResponseDto>>;
}
