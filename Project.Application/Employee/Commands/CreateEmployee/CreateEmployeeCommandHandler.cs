using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Application.Mapping.Employee;

namespace Project.Application.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ErrorOr<Domain.Employee>>
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

        public async Task<ErrorOr<Domain.Employee>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var id = _employeeRepository.GetMaxId();
                var employeemapper = request.EmployeeDTO.AddEmployeeMapper();

                employeemapper.EmployeeID = id + 1;
                employeemapper.EmployeeGuid = Guid.NewGuid();

                await _employeeRepository.AddEmployeeAsync(employeemapper);
                await _unitOfWork.CommitAsync();
                return employeemapper;
            }
            catch
            { 
                await _unitOfWork.RollbackAsync();
                return Error.Failure("CreateEmployeeError");
            }
        }
    }
}
