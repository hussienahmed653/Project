// مثال كامل لعملية Update في Clean Architecture

// =======================
// Domain Layer
// =======================

// Domain/Entities/User.cs
namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}


// =======================
// Application Layer
// =======================

// Application/Interfaces/IUserRepository.cs
namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
    }
}

// Application/Features/Users/Commands/UpdateUser/UpdateUserCommand.cs
using MediatR;

namespace Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(Guid Id, string Name, int Age) : IRequest;
}

// Application/Features/Users/Commands/UpdateUser/UpdateUserHandler.cs
using Application.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _repository;

        public UpdateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if (user == null)
                throw new Exception("User not found");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new Exception("Name cannot be empty");

            user.Name = request.Name;
            user.Age = request.Age;

            await _repository.UpdateAsync(user);
            return Unit.Value;
        }
    }
}


// =======================
// Infrastructure Layer
// =======================

// Infrastructure/Repositories/UserRepository.cs
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}


// =======================
// Web API Layer
// =======================

// WebAPI/Controllers/UsersController.cs
using Application.Features.Users.Commands.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            await _mediator.Send(new UpdateUserCommand(id, request.Name, request.Age));
            return NoContent();
        }
    }

    public class UpdateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}


// =======================
// Notes
// =======================
// - Domain: يحتوي فقط على الكيانات (Entities)
// - Application: يحتوي على الواجهات (interfaces) والـ Use Cases (commands + handlers)
// - Infrastructure: تنفذ الوصول للبيانات
// - WebAPI: تستقبل الطلبات وترد على العميل

// هذا الهيكل بيوضح الفصل بين الطبقات بشكل واضح ونظيف
