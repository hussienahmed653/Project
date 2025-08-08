using Project.Domain;

namespace Project.Application.DTOs
{
    public class EmployeeResponseDto
    {
        public Guid EmployeeGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Title { get; set; }
        public string? TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? HomePhone { get; set; }
        public string? Extension { get; set; }
        public string? Notes { get; set; }
        public int? ReportsTo { get; set; }
        public List<string>? Filepath { get; set; } = new List<string>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<EmployeeTerritorie> EmployeeTerritories { get; set; } = new List<EmployeeTerritorie>();
    }
}
