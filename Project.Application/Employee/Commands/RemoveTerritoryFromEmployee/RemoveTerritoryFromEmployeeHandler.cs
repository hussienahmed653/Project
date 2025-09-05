using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Employee.Commands.RemoveTerritoryFromEmployee
{
    public class RemoveTerritoryFromEmployeeHandler : IRequestHandlerRepository<RemoveTerritoryFromEmployeeCommand, ErrorOr<Deleted>>
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

        public async Task<ErrorOr<Deleted>> Handle(RemoveTerritoryFromEmployeeCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                /*
                    1 success
                    0 employee guid is wrong
                    -1 territory id is wrong
                    -2 the employee is already assigned to the specified territory
                 */

                var error = await _employeeTerritorieRepository.RemoveTerritoryFromEmployee(request.EmpGuid, request.TerId);


                if (error is 0)
                    return Error.NotFound("NotFound", "There is no Employee with this guid");
                if (error is -1)
                    return Error.NotFound("NotFound", "There is no Territory with this id");
                if (error is -2)
                    return Error.Unexpected("UnexpectedError", "There is no employee assigned to the specified territory.");
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
