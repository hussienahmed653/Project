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
        public static List<EmployeeResponseDto> GetEmployee(this List<Domain.ViewEmployeeData> employee)
        {
            var groupemployee = employee
                .GroupBy(e => e.EmployeeGuid)
                .Select(g => new EmployeeResponseDto
                {
                    EmployeeGuid = g.Key,
                    Address = g.First().Address,
                    City = g.First().City,
                    Country = g.First().Country,
                    Extension = g.First().Extension,
                    FirstName = g.First().FirstName,
                    HomePhone = g.First().HomePhone,
                    LastName = g.First().LastName,
                    PostalCode = g.First().PostalCode,
                    Region = g.First().Region,
                    Title = g.First().Title,
                    TitleOfCourtesy = g.First().TitleOfCourtesy,
                    BirthDate = g.First().BirthDate,
                    HireDate = g.First().HireDate,
                    Notes = g.First().Notes,
                    Filepath = g.Select(p => p.Path).ToList(),
                    EmployeeTerritories = new List<Domain.EmployeeTerritorie>
                    {
                        new Domain.EmployeeTerritorie
                        {
                            TerritoryID = g.First().TerritoryID,
                            territorie = new Domain.Territorie
                            {
                                TerritoryDescription = g.First().TerritoryDescription,
                                RegionID = g.First().RegionID,
                                Region = new Domain.Region
                                {
                                    RegionDescription = g.First().RegionDescription
                                }
                            }
                        }
                    },
                    
                }).ToList();
            return groupemployee;
        }
    }
}
