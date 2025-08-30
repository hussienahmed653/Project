using ErrorOr;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;

namespace Project.Application.Employee.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(AddEmployeeDto EmployeeDTO) : IRequestRepository<ErrorOr<Domain.Employee>>;
}
