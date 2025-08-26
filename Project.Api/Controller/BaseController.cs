using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Project.Api.Controller
{
    public class BaseController : ControllerBase
    {
        protected IActionResult ProblemOr<T>(ErrorOr<T> result)
        {
            if(result.IsError)
            {
                return Problem(result.Errors.ToList());
            }


            if (result.Value is Deleted)
                return NoContent();
            if(result.Value is Created)
                return Created("Created","Territory Added To Employee");
            if (result.Value is Updated)
                return NoContent();


            return Ok(result.Value);
        }


        private IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
            {
                return base.Problem();
            }

            if (errors.All(error => error.Type == ErrorType.NotFound))
            {
                return NotFound(errors);
            }
            else if(errors.All(error => error.Type == ErrorType.Validation))
            {
                return StatusCode(StatusCodes.Status400BadRequest, errors);
            }
            else if (errors.All(error => error.Type == ErrorType.Unauthorized))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, errors);
            }
            else if(errors.All(errors => errors.Type == ErrorType.Forbidden))
            {
                return StatusCode(StatusCodes.Status403Forbidden, errors);
            }
            else if (errors.All(errors => errors.Type == ErrorType.Conflict))
            {
                return StatusCode(StatusCodes.Status409Conflict, errors);
            }
            else if (errors.All(error => error.Type == ErrorType.Unexpected))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }

            return base.Problem();
        }
    }
}
