using AutoMapper;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;
using System.Runtime.CompilerServices;

namespace Project.Application.Employee.Queries.GetEmployee
{
    public class GetEmployeeQueriesHandler : IRequestHandler<GetEmployeeQueries, List<EmployeeResponseDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _entityFileRepository;
        private readonly IMapper _mapper;

        public GetEmployeeQueriesHandler(IEmployeeRepository employeeRepository,
            IMapper mapper,
            IEntityFileRepository entityFileRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _entityFileRepository = entityFileRepository;
        }

        public async Task<List<EmployeeResponseDto>> Handle(GetEmployeeQueries request, CancellationToken cancellationToken)
        {
            if(request.Guid is null)
            {
                var listofemployees = await _employeeRepository.GetAllEmployeesAsync();

                if(listofemployees.Count is 0)
                    throw new Exception("There is no Employees found.");

                var listofemployeesmapper = _mapper.Map<List<EmployeeResponseDto>>(listofemployees);
                return listofemployeesmapper;
            }
            var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.Guid);
            if (employee is null)
                throw new Exception("There is no Employee With This Guid");
            var employeemapper = _mapper.Map<EmployeeResponseDto>(employee);
            return new List<EmployeeResponseDto> 
            { 
                employeemapper 
            };
        }
    }
}
