using AutoMapper;
using Azure.Core;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;
using Project.Domain;

namespace Project.Application.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeResponseDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeResponseDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.UpdateEmployeeDTO.EmployeeGuid);
            if (employee is null)
                throw new FileNotFoundException("There is no Employee With This Guid");
            _mapper.Map(request.UpdateEmployeeDTO, employee);
            await _employeeRepository.UpdateEmployeeAsync(employee.Employee);
            var employeemapper = _mapper.Map<EmployeeResponseDto>(employee);
            return employeemapper;
        }

        //public Task<Domain.Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        //{
        //    var employee = new Domain.Employee
        //    {
        //        FirstName = request.EmployeeDTO.FirstName,
        //        LastName = request.EmployeeDTO.LastName,
        //        BirthDate = request.EmployeeDTO.BirthDate,
        //        Address = request.EmployeeDTO.Address,
        //        Country = request.EmployeeDTO.Country,
        //        City = request.EmployeeDTO.City,
        //        HireDate = request.EmployeeDTO.HireDate,
        //        HomePhone = request.EmployeeDTO.HomePhone,
        //        Notes = request.EmployeeDTO.Notes,
        //        PostalCode = request.EmployeeDTO.PostalCode,
        //        Region = request.EmployeeDTO.Region,
        //        Title = request.EmployeeDTO.Title,
        //        Extension = request.EmployeeDTO.Extension,
        //        TitleOfCourtesy = request.EmployeeDTO.TitleOfCourtesy,
        //        ReportsTo = request.EmployeeDTO.ReportsTo,
        //    };
        //    _employeeRepository.UpdateEmployeeAsync(employee);
        //}
    }
}
