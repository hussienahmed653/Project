using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
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
            //try
            //{
            var command = new CreateEmployeeCommand(addEmployeeDTO);
            var result = await _mediator.Send(command);
            //return Ok(new
            //{
            //    iserror = result.IsError,
            //    value = result.Value,
            //    errortype = result.Errors
            //});
            return ProblemOr(result);
            //}
            //catch (Exception ex)
            //{
            //    return Problem(statusCode: 404);
            //}
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
            var command = new UpdateEmployeeCommand(updateEmployeeDto);
            var result = await _mediator.Send(command);
            return ProblemOr(result);
        }

    }
}
