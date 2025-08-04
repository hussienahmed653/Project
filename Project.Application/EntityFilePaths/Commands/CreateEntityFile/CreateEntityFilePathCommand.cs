using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Project.Application.EntityFilePaths.Commands.CreateEntityFile
{
    public record CreateEntityFilePathCommand(Guid Guid, IFormFile File) : IRequest<ErrorOr<string>>;
}
