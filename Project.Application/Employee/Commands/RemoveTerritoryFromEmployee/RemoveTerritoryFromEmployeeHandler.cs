using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;

namespace Project.Application.Employee.Commands.RemoveTerritoryFromEmployee
{
    public class RemoveTerritoryFromEmployeeHandler : IRequestHandler<RemoveTerritoryFromEmployeeCommand, ErrorOr<Deleted>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeTerritorieRepository _employeeTerritorieRepository;
        private readonly ITerritorieRepository _territorieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTerritoryFromEmployeeHandler(IUnitOfWork unitOfWork,
            IEmployeeRepository employeeRepository,
            IEmployeeTerritorieRepository employeeTerritorieRepository,
            ITerritorieRepository territorieRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _employeeTerritorieRepository = employeeTerritorieRepository;
            _territorieRepository = territorieRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(RemoveTerritoryFromEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if(!await _employeeRepository.ExistAsync(request.EmpGuid))
                    return Error.NotFound("NotFound", "There is no Employee with this guid");

                var employee = await _employeeRepository.GetTableEmployeesAsync(request.EmpGuid);

                if(!await _territorieRepository.ExistAsync(request.TerId))
                    return Error.NotFound("NotFound", "There is no Territory with this id");

                if (!await _employeeTerritorieRepository.ExistAsync(request.TerId, employee.EmployeeID))
                    return Error.Unexpected("UnexpectedError", "The employee is not assigned to the specified territory.");

                await _employeeTerritorieRepository.RemoveTerritoryFromEmployee(employee.EmployeeTerritories.FirstOrDefault(e => e.TerritoryID == request.TerId));
                await _unitOfWork.CommitAsync();
                return Result.Deleted;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("Can't remove");
            }
        }
    }
}
