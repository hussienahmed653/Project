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
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericFileService<TEntity> where TEntity : class
    {
        Task<TEntity> SaveFileBytesAndAttachAsync(
            IFormFile file,
            TEntity entity,
            Expression<Func<TEntity, byte[]>> byteArrayProperty
        );
    }
}


// Application/Features/FileUpload/Commands/UploadEntityFileCommand.cs
using Microsoft.AspNetCore.Http;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.FileUpload.Commands
{
    public class UploadEntityFileCommand<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public IFormFile File { get; }
        public TEntity Entity { get; }
        public Expression<Func<TEntity, byte[]>> ByteArrayProperty { get; }

        public UploadEntityFileCommand(IFormFile file, TEntity entity, Expression<Func<TEntity, byte[]>> byteArrayProperty)
        {
            File = file;
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
            return await _fileService.SaveFileBytesAndAttachAsync(
                request.File,
                request.Entity,
                request.ByteArrayProperty
            );
        }
    }
}


// ✅ INFRASTRUCTURE LAYER

// Infrastructure/Services/GenericFileService.cs
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
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

        public async Task<TEntity> SaveFileBytesAndAttachAsync(
            IFormFile file,
            TEntity entity,
            Expression<Func<TEntity, byte[]>> byteArrayProperty
        )
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var memberExpression = byteArrayProperty.Body as MemberExpression;
            var propertyInfo = memberExpression?.Member as PropertyInfo;

            if (propertyInfo == null)
                throw new InvalidOperationException("Invalid property expression.");

            propertyInfo.SetValue(entity, memoryStream.ToArray());

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

        public FileUploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload-trainer")]
        public async Task<IActionResult> UploadTrainer([FromForm] IFormFile file, [FromForm] string name)
        {
            var trainer = new Trainer
            {
                Name = name
            };

            var result = await _mediator.Send(new UploadEntityFileCommand<Trainer>(
                file,
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
