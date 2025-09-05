using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.Mapping.Employee;

namespace Project.Application.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandlerRepository<CreateEmployeeCommand, ErrorOr<Domain.Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".png", ".gif" };

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Domain.Employee>> Handle(CreateEmployeeCommand request)
        {
            try
            {
                 await _unitOfWork.BeginTransactionAsync();

                if (request.EmployeeDTO.HireDate > DateTime.Now || request.EmployeeDTO.BirthDate > DateTime.Now)
                    return Error.Validation(code: "Validation Type Error", "Hire and Birth date cannot be in the future.");
                if (request.EmployeeDTO.Extension is not null && !_allowedExtensions.Contains(request.EmployeeDTO.Extension))
                    return Error.Validation("Validation Type Error", "Extension is not allowed. , It must be { " + $"{string.Join(", ", _allowedExtensions)}" + " }");

                request.EmployeeDTO.EmployeeGuid = Guid.NewGuid();


                var id = await _employeeRepository.AddEmployeeAsync(request.EmployeeDTO);
                if(id is -1)
                    return Error.Validation(description: "ReportsTo should be a valid number");

                request.EmployeeDTO.EmployeeID = id;
                var employeemapper = request.EmployeeDTO.AddEmployeeMapper();

                await _unitOfWork.CommitAsync();
                return employeemapper;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure(description: "CreateEmployeeError");
            }
        }
    }
}
