using ErrorOr;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Employee.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid Guid) : IRequestRepository<ErrorOr<Deleted>>;
}
