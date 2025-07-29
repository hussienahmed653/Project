using Mapster;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Domain;

namespace Project.Application.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Domain.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityFileRepository _genericUploadeEntityFile;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEntityFileRepository genericUploadeEntityFile)
        {
            _employeeRepository = employeeRepository;
            _genericUploadeEntityFile = genericUploadeEntityFile;
        }

        public async Task<Domain.Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var id = _employeeRepository.GetMaxId();
                //var employeemapper = _mapper.Map<Domain.Employee>(request.EmployeeDTO);
                var employeemapper = request.EmployeeDTO.Adapt<Domain.Employee>();
                employeemapper.EmployeeID = id + 1;
                employeemapper.EmployeeGuid = Guid.NewGuid();
                //var employee = new Domain.Employee
                //{
                //    EmployeeID = id + 1,
                //    EmployeeGuid = Guid.NewGuid(),
                //    FirstName = request.EmployeeDTO.FirstName,
                //    LastName = request.EmployeeDTO.LastName,
                //    BirthDate = request.EmployeeDTO.BirthDate,
                //    Address = request.EmployeeDTO.Address,
                //    Country = request.EmployeeDTO.Country,
                //    City = request.EmployeeDTO.City,
                //    HireDate = request.EmployeeDTO.HireDate,
                //    HomePhone = request.EmployeeDTO.HomePhone,
                //    Notes = request.EmployeeDTO.Notes,
                //    PostalCode = request.EmployeeDTO.PostalCode,
                //    Region = request.EmployeeDTO.Region,
                //    Title = request.EmployeeDTO.Title,
                //    Extension = request.EmployeeDTO.Extension,
                //    TitleOfCourtesy = request.EmployeeDTO.TitleOfCourtesy,
                //    ReportsTo = request.EmployeeDTO.ReportsTo
                //};
                await _employeeRepository.AddEmployeeAsync(employeemapper);

                var employeefilepath = new FilePath
                {
                    EntityGuid = employeemapper.EmployeeGuid,
                };
                await _genericUploadeEntityFile.UploadFileAsync(request.EmployeeDTO.file, employeefilepath, employeemapper.FirstName);
                return employeemapper;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the employee: {ex.Message}");
            }
        }
    }
}
