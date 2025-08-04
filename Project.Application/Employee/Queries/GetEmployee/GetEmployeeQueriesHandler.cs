using ErrorOr;
using Mapster;
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
                        throw new Exception("There is no Employees found.");
                    await _unitOfWork.CommitAsync();
                    //return listofemployees.Adapt<List<EmployeeResponseDto>>();
                    return listofemployees.GetEmployee();
                }
                var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.Guid);
                if (employee is null)
                    throw new Exception("There is no Employee With This Guid");
                //var employeemapper = employee.Adapt<EmployeeResponseDto>();
                var employeemapper = employee.GetEmployee();
                await _unitOfWork.CommitAsync();
                return new List<EmployeeResponseDto> { employeemapper };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("An error occurred while retrieving the employee");
            }
        }
    }
}

