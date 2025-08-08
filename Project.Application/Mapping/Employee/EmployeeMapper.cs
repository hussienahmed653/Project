using Project.Application.DTOs;

namespace Project.Application.Mapping.Employee
{
    public static class EmployeeMapper
    {
        public static Domain.Employee AddEmployeeMapper(this AddEmployeeDto addEmployeeDto)
        {
            var employee = new Domain.Employee();
            employee.LastName = addEmployeeDto.LastName;
            employee.FirstName = addEmployeeDto.FirstName;
            employee.Title = addEmployeeDto.Title;
            employee.TitleOfCourtesy = addEmployeeDto.TitleOfCourtesy;
            employee.BirthDate = addEmployeeDto.BirthDate;
            employee.HireDate = addEmployeeDto.HireDate;
            employee.Address = addEmployeeDto.Address;
            employee.City = addEmployeeDto.City;
            employee.Region = addEmployeeDto.Region;
            employee.PostalCode = addEmployeeDto.PostalCode;
            employee.Country = addEmployeeDto.Country;
            employee.HomePhone = addEmployeeDto.HomePhone;
            employee.Extension = addEmployeeDto.Extension;
            employee.Notes = addEmployeeDto.Notes;
            employee.ReportsTo = addEmployeeDto.ReportsTo;

            return employee;
        }

        public static EmployeeResponseDto GetEmployee(this Domain.Employee employee)
        {
            var employeeResponse = new EmployeeResponseDto();
            employeeResponse.EmployeeGuid = employee.EmployeeGuid;
            employeeResponse.HomePhone = employee.HomePhone;
            employeeResponse.FirstName = employee.FirstName;
            employeeResponse.LastName = employee.LastName;
            employeeResponse.Title = employee.Title;
            employeeResponse.TitleOfCourtesy = employee.TitleOfCourtesy;
            employeeResponse.BirthDate = employee.BirthDate;
            employeeResponse.HireDate = employee.HireDate;
            employeeResponse.Address = employee.Address;
            employeeResponse.City = employee.City;
            employeeResponse.Region = employee.Region;
            employeeResponse.PostalCode = employee.PostalCode;
            employeeResponse.Country = employee.Country;
            employeeResponse.Extension = employee.Extension;
            employeeResponse.Notes = employee.Notes;
            employeeResponse.Filepath = employee.EntityFiles?.Select(f => f.Path).ToList();

            return employeeResponse;
        }
        public static List<EmployeeResponseDto> GetEmployee(this List<Domain.Employee> employee)
        {
            var employeeResponse = new List<EmployeeResponseDto>();
            foreach (var item in employee)
            {
                var emp = new EmployeeResponseDto
                {
                    EmployeeGuid = item.EmployeeGuid,
                    HomePhone = item.HomePhone,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Title = item.Title,
                    TitleOfCourtesy = item.TitleOfCourtesy,
                    BirthDate = item.BirthDate,
                    HireDate = item.HireDate,
                    Address = item.Address,
                    City = item.City,
                    Region = item.Region,
                    PostalCode = item.PostalCode,
                    Country = item.Country,
                    Extension = item.Extension,
                    Notes = item.Notes,
                    Filepath = item.EntityFiles?.Select(f => f.Path).ToList(),
                    Orders = item.Orders.ToList(),
                    EmployeeTerritories = item.EmployeeTerritories.ToList()
                };
                employeeResponse.Add(emp);
            }
            return employeeResponse;
        }
    }
}
