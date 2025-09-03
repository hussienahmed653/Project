using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.Mapping.Employee;

namespace Project.Application.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandlerRepository<CreateEmployeeCommand, ErrorOr<Domain.Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _genericUploadeEntityFile;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".png", ".gif" };

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            IEntityFileRepository genericUploadeEntityFile,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _genericUploadeEntityFile = genericUploadeEntityFile;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Domain.Employee>> Handle(CreateEmployeeCommand request)
        {
            try
            {
                 await _unitOfWork.BeginTransactionAsync();

                //var id = _employeeRepository.GetMaxId();

                if (request.EmployeeDTO.HireDate > DateTime.Now || request.EmployeeDTO.BirthDate > DateTime.Now)
                    return Error.Validation(code: "Validation Type Error", "Hire and Birth date cannot be in the future.");


                request.EmployeeDTO.EmployeeGuid = Guid.NewGuid();

                //if (request.EmployeeDTO is { ReportsTo: <= 0 } || request.EmployeeDTO.ReportsTo > id)
                //    return Error.Validation("Validation Type Error", "ReportsTo must reference an existing Employee ID.");

                var id = await _employeeRepository.AddEmployeeAsync(request.EmployeeDTO);
                request.EmployeeDTO.EmployeeID = id;
                var employeemapper = request.EmployeeDTO.AddEmployeeMapper();

                if (request.EmployeeDTO.Extension is not null && !_allowedExtensions.Contains(request.EmployeeDTO.Extension))
                    return Error.Validation("Validation Type Error", "Extension is not allowed. , It must be { " + $"{string.Join(", ", _allowedExtensions)}" + " }");
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
