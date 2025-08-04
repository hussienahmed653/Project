using ErrorOr;
using MediatR;
using Project.Application.DTOs;

namespace Project.Application.Employee.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(UpdateEmployeeDto UpdateEmployeeDTO) : IRequest<ErrorOr<EmployeeResponseDto>>;
}
