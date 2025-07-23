using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;

namespace Project.Application.Employee.Queries.GetEmployee
{
    public class GetEmployeeQueriesHandler : IRequestHandler<GetEmployeeQueries, List<Domain.Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeQueriesHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Domain.Employee>> Handle(GetEmployeeQueries request, CancellationToken cancellationToken)
        {
            if(request.Guid is null)
            {
                var listofemployees = await _employeeRepository.GetAllEmployeesAsync();
                if(listofemployees.Count is 0)
                    throw new Exception("There is no Employees found.");
                return listofemployees;
            }
            var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.Guid);
            if (employee is null)
                throw new Exception("There is no Employee With This Guid");
            return new List<Domain.Employee> { employee };
        }
    }
}
