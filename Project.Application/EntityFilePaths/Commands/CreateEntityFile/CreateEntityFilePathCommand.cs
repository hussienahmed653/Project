using ErrorOr;
using Microsoft.AspNetCore.Http;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.EntityFilePaths.Commands.CreateEntityFile
{
    public record CreateEntityFilePathCommand(Guid Guid, IFormFile File) : IRequestRepository<ErrorOr<string>>;
}
