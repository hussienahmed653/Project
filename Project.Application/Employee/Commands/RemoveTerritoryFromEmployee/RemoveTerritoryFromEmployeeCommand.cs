using ErrorOr;
using MediatR;

namespace Project.Application.Employee.Commands.RemoveTerritoryFromEmployee
{
    public record RemoveTerritoryFromEmployeeCommand(Guid EmpGuid, int TerId) : IRequest<ErrorOr<Deleted>>;
}
