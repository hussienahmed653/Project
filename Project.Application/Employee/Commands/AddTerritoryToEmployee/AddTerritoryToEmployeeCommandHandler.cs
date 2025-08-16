using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;

namespace Project.Application.Employee.Commands.AddTerritoryToEmployee
{
    public class AddTerritoryToEmployeeCommandHandler : IRequestHandler<AddTerritoryToEmployeeCommand, ErrorOr<Created>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeTerritorieRepository _EmployeeterritoryRepository;
        private readonly ITerritorieRepository _territoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTerritoryToEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            IEmployeeTerritorieRepository EmployeeterritoryRepository,
            ITerritorieRepository territoryRepository)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _EmployeeterritoryRepository = EmployeeterritoryRepository;
            _territoryRepository = territoryRepository;
        }

        public async Task<ErrorOr<Created>> Handle(AddTerritoryToEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (!await _employeeRepository.ExistAsync(request.Guid))
                    return Error.NotFound("NotFound", "There is no Employee with this guid");


                var employee = await _employeeRepository.GetTableViewEmployeeByGuIdAsync(request.Guid);

                if(!await _territoryRepository.ExistAsync(request.id))
                    return Error.NotFound("NotFound", "There is no Territory with this id");

                if (await _EmployeeterritoryRepository.ExistAsync(request.id , employee.Select(e => e.EmployeeID).FirstOrDefault()))
                    return Error.Unexpected("UnexpectedError", "The employee is assigned to the specified territory.");

                var employeeterritoy = new Domain.EmployeeTerritorie
                {
                    EmployeeID = request.id,
                    TerritoryID = request.id
                };
                await _EmployeeterritoryRepository.AddTerritoryToEmployee(employee.Select(e => e.EmployeeID).FirstOrDefault(), employeeterritoy);

                await _unitOfWork.CommitAsync();
                return Result.Created;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("CreateEmployeeTerritoryError");
            }
        }
    }
}
