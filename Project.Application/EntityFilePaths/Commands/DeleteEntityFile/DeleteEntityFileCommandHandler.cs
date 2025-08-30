using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.EntityFilePaths.Commands.DeleteEntityFile
{
    public class DeleteEntityFileCommandHandler : IRequestHandlerRepository<DeleteEntityFileCommand, ErrorOr<bool>>
    {
        private readonly IEntityFileRepository _entityFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEntityFileCommandHandler(IEntityFileRepository entityFileRepository,
            IUnitOfWork unitOfWork)
        {
            _entityFileRepository = entityFileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteEntityFileCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if (!await _entityFileRepository.FileIsExistAsync(request.Guid))
                    return Error.NotFound(code: "FileNotFound", description: "The file with the specified GUID does not exist.");
                await _entityFileRepository.DeleteFileAsync(request.Guid);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("DeleteFileError", ex.Message);
            }
        }
    }
}
