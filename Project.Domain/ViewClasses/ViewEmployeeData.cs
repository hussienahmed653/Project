using System.ComponentModel.DataAnnotations;

namespace Project.Domain.ViewClasses
{
    public class ViewEmployeeData
    {
        public int EmployeeID { get; set; }
        public Guid EmployeeGuid { get; set; }
        [MaxLength(20)]
        public string? LastName { get; set; }
        [MaxLength(10)]
        public string? FirstName { get; set; }
        [MaxLength(30)]
        public string? Title { get; set; }
        [MaxLength(25)]
        public string? TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        [MaxLength(60)]
        public string? Address { get; set; }
        [MaxLength(15)]
        public string? City { get; set; }
        [MaxLength(15)]
        public string? Region { get; set; }
        [MaxLength(10)]
        public string? PostalCode { get; set; }
        [MaxLength(15)]
        public string? Country { get; set; }
        [MaxLength(24)]
        public string? HomePhone { get; set; }
        [MaxLength(4)]
        public string? Extension { get; set; }
        public string? Notes { get; set; }
        public int? ReportsTo { get; set; }
        public string? Path { get; set; }
        public int? TerritoryID { get; set; }
        [MaxLength(50)]
        public string? TerritoryDescription { get; set; }
        public int? RegionID { get; set; }
        [MaxLength(50)]
        public string? RegionDescription { get; set; }
    }
}
