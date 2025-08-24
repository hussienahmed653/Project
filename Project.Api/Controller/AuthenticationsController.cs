using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Authentication.Command;
using Project.Application.Authentication.Dtos;
using Project.Application.Authentication.Queries;

namespace Project.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : BaseController
    {
        private readonly ISender _mediator;
        public AuthenticationsController(ISender mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await _mediator.Send(new RegisterCommand(registerRequest));
            return ProblemOr(result);
            /*
             
            {
                "userId": "c18db3e1-a402-447d-9bdd-07d6139e3259",
                "firstName": "hussien",
                "lastName": "ahmed",
                "email": "hussienahmed1122006@gmail.com",
                "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJjMThkYjNlMS1hNDAyLTQ0N2QtOWJkZC0wN2Q2MTM5ZTMyNTkiLCJuYW1lIjoiaHVzc2llbiIsImZhbWlseV9uYW1lIjoiYWhtZWQiLCJlbWFpbCI6Imh1c3NpZW5haG1lZDExMjIwMDZAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTc1NjA2MDc5NiwiaXNzIjoiUHJvamVjdCIsImF1ZCI6IlByb2plY3QifQ.Cy4OTTSD1c6lSteivkFiEVQ4DGOTdhjhD4WfAt7pbvo"
            }
             
             */
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var result = await _mediator.Send(new LoginQuerie(loginRequest));
            return ProblemOr(result);
        }
    }
}
