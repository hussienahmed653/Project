using ErrorOr;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Employee.Commands.AddTerritoryToEmployee
{
    public record AddTerritoryToEmployeeCommand(Guid Guid, int id) : IRequestRepository<ErrorOr<Created>>;
}
