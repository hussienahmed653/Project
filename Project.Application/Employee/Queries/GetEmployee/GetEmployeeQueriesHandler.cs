using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;
using Project.Application.Mapping.Employee;

namespace Project.Application.Employee.Queries.GetEmployee
{
    public class GetEmployeeQueriesHandler : IRequestHandler<GetEmployeeQueries, ErrorOr<List<EmployeeResponseDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _entityFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeeQueriesHandler(IEmployeeRepository employeeRepository,
            IEntityFileRepository entityFileRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _entityFileRepository = entityFileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<EmployeeResponseDto>>> Handle(GetEmployeeQueries request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if (request.Guid is null)
                {
                    var listofemployees = await _employeeRepository.GetAllEmployeesAsync();

                    if (listofemployees.Count is 0)
                        return Error.NotFound(code: "NotFound", description: "There is no Employee");
                    //return listofemployees.Adapt<List<EmployeeResponseDto>>();
                    var listofemployeesmapper = listofemployees.GetAllEmployees();
                    await _unitOfWork.CommitAsync();
                    return listofemployeesmapper;
                }
                var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.Guid);
                if (employee.Count is 0)
                    return Error.NotFound(code: "NotFound", description: "There is no Employee With This Guid");
                //var employeemapper = employee.Adapt<EmployeeResponseDto>();
                var employeemapper = employee.GetSingleEmployee();
                await _unitOfWork.CommitAsync();
                return new List<EmployeeResponseDto> { employeemapper };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure();
            }
        }
    }
}

