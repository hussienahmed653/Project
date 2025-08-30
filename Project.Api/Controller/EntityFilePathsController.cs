using Microsoft.AspNetCore.Mvc;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.EntityFilePaths.Commands.CreateEntityFile;
using Project.Application.EntityFilePaths.Commands.DeleteEntityFile;

namespace Project.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityFilePathsController : BaseController
    {
        private readonly IMediatorRepository _mediator;

        public EntityFilePathsController(IMediatorRepository mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, Guid guid)
        {
            try
            {
                var command = new CreateEntityFilePathCommand(guid, file);
                var result = await _mediator.Send(command);
                return ProblemOr(result);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
        [HttpDelete("DeleteFile/{guid}")]
        public async Task<IActionResult> DeleteFile(Guid guid)
        {
            try
            {
                var result = await _mediator.Send(new DeleteEntityFileCommand(guid));
                return ProblemOr(result);
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
