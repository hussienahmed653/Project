using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.DTOs;

namespace Project.Application.Employee.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(AddEmployeeDTO EmployeeDTO) : IRequest<ErrorOr<Domain.Employee>>;
}
