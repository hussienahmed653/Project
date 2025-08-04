// ✅ DOMAIN LAYER (Example Entity)
// Domain/Entities/Trainer.cs
public class Trainer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] ImageBytes { get; set; }
}


// ✅ APPLICATION LAYER

// Application/Interfaces/IGenericFileService.cs
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericFileService<TEntity> where TEntity : class
    {
        Task<TEntity> SaveFileAsync(
            byte[] fileBytes,
            TEntity entity,
            Expression<Func<TEntity, byte[]>> byteArrayProperty
        );
    }
}


// Application/Features/FileUpload/Commands/UploadEntityFileCommand.cs
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.FileUpload.Commands
{
    public class UploadEntityFileCommand<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public byte[] FileBytes { get; }
        public TEntity Entity { get; }
        public Expression<Func<TEntity, byte[]>> ByteArrayProperty { get; }

        public UploadEntityFileCommand(byte[] fileBytes, TEntity entity, Expression<Func<TEntity, byte[]>> byteArrayProperty)
        {
            FileBytes = fileBytes;
            Entity = entity;
            ByteArrayProperty = byteArrayProperty;
        }
    }
}


// Application/Features/FileUpload/Commands/UploadEntityFileHandler.cs
using Application.Interfaces;
using MediatR;

namespace Application.Features.FileUpload.Commands
{
    public class UploadEntityFileHandler<TEntity> : IRequestHandler<UploadEntityFileCommand<TEntity>, TEntity>
        where TEntity : class
    {
        private readonly IGenericFileService<TEntity> _fileService;

        public UploadEntityFileHandler(IGenericFileService<TEntity> fileService)
        {
            _fileService = fileService;
        }

        public async Task<TEntity> Handle(UploadEntityFileCommand<TEntity> request, CancellationToken cancellationToken)
        {
            return await _fileService.SaveFileAsync(
                request.FileBytes,
                request.Entity,
                request.ByteArrayProperty
            );
        }
    }
}


// ✅ INFRASTRUCTURE LAYER

// Infrastructure/Services/GenericFileService.cs
using Application.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Services
{
    public class GenericFileService<TEntity> : IGenericFileService<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        public GenericFileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> SaveFileAsync(
            byte[] fileBytes,
            TEntity entity,
            Expression<Func<TEntity, byte[]>> byteArrayProperty
        )
        {
            var memberExpression = byteArrayProperty.Body as MemberExpression;
            var propertyInfo = memberExpression?.Member as PropertyInfo;

            if (propertyInfo == null)
                throw new InvalidOperationException("Invalid property expression.");

            propertyInfo.SetValue(entity, fileBytes);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}


// ✅ WEBAPI LAYER

// WebAPI/Controllers/FileUploadController.cs
using Application.Features.FileUpload.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload-trainer")]
        public async Task<IActionResult> UploadTrainer([FromForm] IFormFile file, [FromForm] string name)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Save physical file in wwwroot/uploads
            var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Extract bytes
            using var ms = new MemoryStream();
            file.Position = 0;
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            var trainer = new Trainer { Name = name };

            var result = await _mediator.Send(new UploadEntityFileCommand<Trainer>(
                fileBytes,
                trainer,
                t => t.ImageBytes
            ));

            return Ok(new
            {
                result.Id,
                result.Name,
                ImageSize = result.ImageBytes?.Length
            });
        }
    }
}


// ✅ REGISTRATION IN Program.cs
// Program.cs
builder.Services.AddScoped(typeof(IGenericFileService<>), typeof(GenericFileService<>));
app.UseStaticFiles();
