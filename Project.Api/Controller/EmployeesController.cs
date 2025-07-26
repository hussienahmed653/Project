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
    public class EmployeesController : ControllerBase
    {
        IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromForm] AddEmployeeDto addEmployeeDTO)
        {
            try
            {
                var command = new CreateEmployeeCommand(addEmployeeDTO);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteEmployee/{guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid guid)
        {
            try
            {
                var result = await _mediator.Send(new DeleteEmployeeCommand(guid));
                if (!result)
                    return BadRequest();
                return Ok(new { Message = "Employee deleted successfully" });
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await _mediator.Send(new GetEmployeeQueries(null));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetEmployeeByGuid/{guid}")]
        public async Task<IActionResult> GetEmployeeByGuid(Guid guid)
        {
            try
            {
                var result = await _mediator.Send(new GetEmployeeQueries(guid));
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromForm] UpdateEmployeeDto updateEmployeeDto)
        {
            try
            {
                var command = new UpdateEmployeeCommand(updateEmployeeDto);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
