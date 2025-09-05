using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Employee.Commands.AddTerritoryToEmployee
{
    public class AddTerritoryToEmployeeCommandHandler : IRequestHandlerRepository<AddTerritoryToEmployeeCommand, ErrorOr<Created>>
    {
        private readonly IEmployeeTerritorieRepository _EmployeeterritoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTerritoryToEmployeeCommandHandler(IUnitOfWork unitOfWork,
            IEmployeeTerritorieRepository EmployeeterritoryRepository)
        {
            _unitOfWork = unitOfWork;
            _EmployeeterritoryRepository = EmployeeterritoryRepository;
        }

        public async Task<ErrorOr<Created>> Handle(AddTerritoryToEmployeeCommand request)
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

                var error = await _EmployeeterritoryRepository.AddTerritoryToEmployee(request.Guid, request.id);

                if(error is 0)
                    return Error.NotFound("NotFound", "There is no Employee with this guid");
                if (error is -1)
                    return Error.NotFound("NotFound", "There is no Territory with this id");
                if (error is -2)
                    return Error.Unexpected("UnexpectedError", "The employee is already assigned to the specified territory.");

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
