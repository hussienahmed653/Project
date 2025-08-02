using Mapster;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Domain;

namespace Project.Application.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Domain.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _genericUploadeEntityFile;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            IEntityFileRepository genericUploadeEntityFile,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _genericUploadeEntityFile = genericUploadeEntityFile;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var id = _employeeRepository.GetMaxId();
                var employeemapper = request.EmployeeDTO.Adapt<Domain.Employee>();
                employeemapper.EmployeeID = id + 1;
                employeemapper.EmployeeGuid = Guid.NewGuid();
                await _employeeRepository.AddEmployeeAsync(employeemapper);

                var employeefilepath = new FilePath
                {
                    EntityGuid = employeemapper.EmployeeGuid,
                };
                await _genericUploadeEntityFile.UploadFileAsync(request.EmployeeDTO.file, employeefilepath, employeemapper.FirstName);
                await _unitOfWork.CommitAsync();
                return employeemapper;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception($"An error occurred while creating the employee: {ex.Message}");
            }
        }
    }
}
