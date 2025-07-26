// Domain Layer
// (No changes required here for file upload)


// Application Layer

// Application/Interfaces/IFileService.cs
namespace Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string subDirectory);
        void DeleteFile(string relativePath);
    }
}

// Application/Features/FileUpload/Commands/UploadFileCommand.cs
using Microsoft.AspNetCore.Http;
using MediatR;

namespace Application.Features.FileUpload.Commands
{
    public record UploadFileCommand(IFormFile File) : IRequest<string>;
}

// Application/Features/FileUpload/Commands/UploadFileHandler.cs
using Application.Interfaces;
using MediatR;

namespace Application.Features.FileUpload.Commands
{
    public class UploadFileHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly IFileService _fileService;

        public UploadFileHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            return await _fileService.SaveFileAsync(request.File, "uploads");
        }
    }
}


// Infrastructure Layer

// Infrastructure/Services/FileService.cs
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subDirectory)
        {
            var uploadPath = Path.Combine(_environment.WebRootPath, subDirectory);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(subDirectory, fileName).Replace("\\", "/");
        }

        public void DeleteFile(string relativePath)
        {
            var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            var folderPath = Path.GetDirectoryName(fullPath);
            if (Directory.Exists(folderPath) && !Directory.EnumerateFileSystemEntries(folderPath).Any())
            {
                Directory.Delete(folderPath);
            }
        }
    }
}


// WebAPI Layer

// Program.cs
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddMediatR(typeof(UploadFileHandler).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();

// WebAPI/Controllers/FileUploadController.cs
using Application.Features.FileUpload.Commands;
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

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var path = await _mediator.Send(new UploadFileCommand(file));
            var url = $"{Request.Scheme}://{Request.Host}/{path}";

            return Ok(new { fileUrl = url });
        }
    }
}
