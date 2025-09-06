using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.EntityFilePaths.Commands.DeleteEntityFile
{
    public class DeleteEntityFileCommandHandler : IRequestHandlerRepository<DeleteEntityFileCommand, ErrorOr<Deleted>>
    {
        private readonly IEntityFileRepository _entityFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEntityFileCommandHandler(IEntityFileRepository entityFileRepository,
            IUnitOfWork unitOfWork)
        {
            _entityFileRepository = entityFileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteEntityFileCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var deleted = await _entityFileRepository.DeleteFileAsync(request.Guid);
                if(deleted is 0)
                    return Error.NotFound(code: "FileNotFound", description: "The file with the specified GUID does not exist.");
                await _unitOfWork.CommitAsync();
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("DeleteFileError", ex.Message);
            }
        }
    }
}
