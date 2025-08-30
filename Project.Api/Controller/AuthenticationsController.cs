using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Authentication.Command.ChangePassword;
using Project.Application.Authentication.Command.Register;
using Project.Application.Authentication.Command.UpdateUserRole;
using Project.Application.Authentication.Dtos;
using Project.Application.Authentication.Queries;
using Project.Application.Common.MediatorInterfaces;
using Project.Domain.Authentication;

namespace Project.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : BaseController
    {
        private readonly IMediatorRepository _mediator;
        public AuthenticationsController(IMediatorRepository mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await _mediator.Send(new RegisterCommand(registerRequest));
            return ProblemOr(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var result = await _mediator.Send(new LoginQuerie(loginRequest));
            return ProblemOr(result);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(string email, Roles roles)
        {
            var result = await _mediator.Send(new UpdateUserRoleCommand(email, roles));
            return ProblemOr(result);
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var result = await _mediator.Send(new ChangePasswordCommand(changePasswordRequest));
            return ProblemOr(result);
        }
    }
}
