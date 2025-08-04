using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Domain;

namespace Project.Application.EntityFilePaths.Commands.CreateEntityFile
{
    public class CreateEntityFileCommandHandler : IRequestHandler<CreateEntityFilePathCommand, ErrorOr<string>>
    {
        private readonly IEntityFileRepository _entityFileRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEntityFileCommandHandler(IEntityFileRepository entityFileRepository,
            IUnitOfWork unitOfWork,
            IEmployeeRepository employeeRepository)
        {
            _entityFileRepository = entityFileRepository;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<string>> Handle(CreateEntityFilePathCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if (!await _employeeRepository.ExistAsync(request.Guid))
                    return Error.NotFound(code: "Guid NotFound", description: "This Guid Is NotFound");
                var entityfilepath = new FilePath
                {
                    EntityGuid = request.Guid
                };
                var filepath = await _entityFileRepository.UploadFileAsync(request.File, entityfilepath);
                await _unitOfWork.CommitAsync();
                return filepath;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("FileUploadError", "An error occurred while uploading the file.");
            }

        }
    }
}
