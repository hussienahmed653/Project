using ErrorOr;
using MediatR;

namespace Project.Application.EntityFilePaths.Commands.DeleteEntityFile
{
    public record DeleteEntityFileCommand(Guid Guid) : IRequest<ErrorOr<bool>>;
}
