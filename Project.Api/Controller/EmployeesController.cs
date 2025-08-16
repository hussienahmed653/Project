using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.Employee.Commands.AddTerritoryToEmployee;
using Project.Application.Employee.Commands.CreateEmployee;
using Project.Application.Employee.Commands.DeleteEmployee;
using Project.Application.Employee.Commands.UpdateEmployee;
using Project.Application.Employee.Queries.GetEmployee;

namespace Project.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDTO)
        {
            var result = await _mediator.Send(new CreateEmployeeCommand(addEmployeeDTO));
            return ProblemOr(result);
        }
        [HttpPost("AddEmployeeToTerritory")]
        public async Task<IActionResult> AddEmployeeToTerritory(Guid EmployeeGuid, int TerritoryId)
        {
            var result = await _mediator.Send(new AddTerritoryToEmployeeCommand(EmployeeGuid, TerritoryId));
            return ProblemOr(result);
        }
        
        [HttpDelete("DeleteEmployee/{guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid guid)
        {
                var result = await _mediator.Send(new DeleteEmployeeCommand(guid));
                return ProblemOr(result);
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
                var result = await _mediator.Send(new GetEmployeeQueries(null));
                return ProblemOr(result);
        }

        [HttpGet("GetEmployeeByGuid/{guid}")]
        public async Task<IActionResult> GetEmployeeByGuid(Guid guid)
        {
                var result = await _mediator.Send(new GetEmployeeQueries(guid));
                return ProblemOr(result);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromForm] UpdateEmployeeDto updateEmployeeDto)
        {
            var result = await _mediator.Send(new UpdateEmployeeCommand(updateEmployeeDto));
            return ProblemOr(result);
        }

    }
}
