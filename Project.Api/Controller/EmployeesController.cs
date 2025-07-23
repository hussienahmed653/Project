using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;
using Project.Application.Employee.Commands.CreateEmployee;
using Project.Application.Employee.Commands.DeleteEmployee;
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
        public async Task<IActionResult> AddEmployee([FromForm] AddEmployeeDTO addEmployeeDTO)
        {
            try
            {
                var command = new CreateEmployeeCommand(addEmployeeDTO);
                var result = await _mediator.Send(command);
                return Ok(
                    new
                    {
                        Message = "Employee added successfully",
                        result = result
                    });
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
                if (result.IsError)
                    return BadRequest();
                return Ok(new { Message = "Employee deleted successfully", result = result.Value });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = _mediator.Send(new GetEmployeeQueries(null));
                return Ok(await result);
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
                var result = _mediator.Send(new GetEmployeeQueries(guid));
                return Ok(await result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
