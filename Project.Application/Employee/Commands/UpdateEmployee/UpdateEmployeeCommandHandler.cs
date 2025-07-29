using Mapster;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;

namespace Project.Application.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeResponseDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        IEntityFileRepository _genericUploadeEntityFile;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            IEntityFileRepository genericUploadeEntityFile)
        {
            _employeeRepository = employeeRepository;
            _genericUploadeEntityFile = genericUploadeEntityFile;
        }

        public async Task<EmployeeResponseDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByGuIdAsync(request.UpdateEmployeeDTO.EmployeeGuid);
            if (employee is null)
                throw new FileNotFoundException("There is no Employee With This Guid");
            //_mapper.Map(request.UpdateEmployeeDTO, employee);
            request.UpdateEmployeeDTO.Adapt(employee);
            if(request.UpdateEmployeeDTO.FirstName is not null)
            {
                var path = employee.EntityFiles.FirstOrDefault()?.Path;
                var newpath = _genericUploadeEntityFile.UpdateFilePath(path, request.UpdateEmployeeDTO.FirstName);
                employee.EntityFiles.FirstOrDefault().Path = newpath.Result;
            }

            await _employeeRepository.UpdateEmployeeAsync(employee);
            //var employeemapper = _mapper.Map<EmployeeResponseDto>(employee);
            var employeemapper = employee.Adapt<EmployeeResponseDto>();
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
