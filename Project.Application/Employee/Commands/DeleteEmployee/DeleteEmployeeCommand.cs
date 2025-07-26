using MediatR;

namespace Project.Application.Employee.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid Guid) : IRequest<bool>;
}
