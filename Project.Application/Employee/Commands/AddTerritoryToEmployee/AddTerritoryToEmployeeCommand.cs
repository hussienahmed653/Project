using ErrorOr;
using MediatR;

namespace Project.Application.Employee.Commands.AddTerritoryToEmployee
{
    public record AddTerritoryToEmployeeCommand(Guid Guid, int id) : IRequest<ErrorOr<Created>>;
}
