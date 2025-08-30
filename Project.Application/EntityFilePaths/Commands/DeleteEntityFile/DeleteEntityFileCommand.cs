using ErrorOr;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.EntityFilePaths.Commands.DeleteEntityFile
{
    public record DeleteEntityFileCommand(Guid Guid) : IRequestRepository<ErrorOr<bool>>;
}
