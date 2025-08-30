using ErrorOr;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Employee.Commands.RemoveTerritoryFromEmployee
{
    public record RemoveTerritoryFromEmployeeCommand(Guid EmpGuid, int TerId) : IRequestRepository<ErrorOr<Deleted>>;
}
