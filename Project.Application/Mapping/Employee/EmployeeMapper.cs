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

        public static List<EmployeeResponseDto> GetAllEmployees(this List<Domain.ViewEmployeeData> employee)
        {
            var groupemployee = employee
                .GroupBy(e => e.EmployeeGuid)
                .Select(g => new EmployeeResponseDto
                {
                    EmployeeGuid = g.Select(e => e.EmployeeGuid).FirstOrDefault(),
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
                    ReportsTo = g.First().ReportsTo,
                    Filepath = g.Select(p => p.Path).ToList()!,
                    TerritoryID = g.Select(t => t.TerritoryID).ToList(),
                    TerritoryDescription = g.Select(t => t.TerritoryDescription).ToList(),
                    RegionID = g.Select(r => r.RegionID).ToList(),
                    RegionDescription = g.Select(t => t.RegionDescription).ToList()
                    
                }).ToList();
            return groupemployee;

            /*
             
            {
                "employeeGuid": "c391e89e-c4d6-4d92-a7c3-04d70024d7a2",
                "firstName": "Hernando",
                "lastName": "Ply",
                "title": "Dr",
                "titleOfCourtesy": "Guatemala",
                "birthDate": "2025-06-23T00:00:00",
                "hireDate": "2024-09-10T00:00:00",
                "address": "Room 713",
                "city": "SantaCruzMuluá",
                "region": "PG-SHM",
                "postalCode": "11006",
                "country": "Peru",
                "homePhone": "397-644-7957",
                "extension": "#1ec",
                "notes": "Dusky rattlesnake",
                "reportsTo": 1,
                "filepath": [
                  null
                ],
                "territoryID": [
                  1
                ],
                "regionID": [
                  1
                ],
                "territoryDescription": [
                  "Fresh Brussels sprouts."
                ],
                "regionDescription": [
                  "Non-slip mat exercises."
                ]
            },
             
             */
        }
        public static EmployeeResponseDto GetSingleEmployee(this List<Domain.ViewEmployeeData> employee)
        {
            return employee.GroupBy(e => e.EmployeeGuid)
                .Select(g => new EmployeeResponseDto
                {
                    EmployeeGuid = g.Select(e => e.EmployeeGuid).FirstOrDefault(),
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
                    ReportsTo = g.First().ReportsTo,
                    Filepath = g.Select(p => p.Path).ToList()!,
                    TerritoryID = g.Select(t => t.TerritoryID).ToList(),
                    TerritoryDescription = g.Select(t => t.TerritoryDescription).ToList()!,
                    RegionID = g.Select(r => r.RegionID).ToList(),
                    RegionDescription = g.Select(t => t.RegionDescription).ToList()
                }).FirstOrDefault()!;
            //var employeeResponse = new EmployeeResponseDto();
            //employeeResponse.EmployeeGuid = employee.EmployeeGuid;
            //employeeResponse.HomePhone = employee.HomePhone;
            //employeeResponse.FirstName = employee.FirstName;
            //employeeResponse.LastName = employee.LastName;
            //employeeResponse.Title = employee.Title;
            //employeeResponse.TitleOfCourtesy = employee.TitleOfCourtesy;
            //employeeResponse.BirthDate = employee.BirthDate;
            //employeeResponse.HireDate = employee.HireDate;
            //employeeResponse.Address = employee.Address;
            //employeeResponse.City = employee.City;
            //employeeResponse.Region = employee.Region;
            //employeeResponse.PostalCode = employee.PostalCode;
            //employeeResponse.Country = employee.Country;
            //employeeResponse.Extension = employee.Extension;
            //employeeResponse.Notes = employee.Notes;
            //employeeResponse.ReportsTo = employee.ReportsTo;
            //employeeResponse.Filepath = employee.EntityFiles?.Select(f => f.Path).ToList();

            //return employeeResponse;
        }
    }
}
