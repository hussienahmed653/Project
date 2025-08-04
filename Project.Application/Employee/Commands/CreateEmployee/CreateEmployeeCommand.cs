using ErrorOr;
using MediatR;
using Project.Application.DTOs;

namespace Project.Application.Employee.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(AddEmployeeDto EmployeeDTO) : IRequest<ErrorOr<Domain.Employee>>;
}
