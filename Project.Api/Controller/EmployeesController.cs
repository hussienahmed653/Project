using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;
using Project.Application.Employee.Commands.CreateEmployee;
using Project.Application.Employee.Commands.DeleteEmployee;

namespace Project.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeRepository _employeeRepository;
        IMediator _mediator;

        public EmployeesController(IEmployeeRepository employeeRepository, IMediator mediator)
        {
            _employeeRepository = employeeRepository;
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

    }
}
