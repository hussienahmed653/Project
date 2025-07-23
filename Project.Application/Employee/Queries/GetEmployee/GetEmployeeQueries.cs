using ErrorOr;
using MediatR;

namespace Project.Application.Employee.Queries.GetEmployee
{
    public record GetEmployeeQueries(Guid? Guid) : IRequest<List<Domain.Employee>>;
}
