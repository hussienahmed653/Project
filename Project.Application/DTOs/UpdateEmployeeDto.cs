using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class UpdateEmployeeDto : BaseEmployeeDto
    {
        public Guid EmployeeGuid { get; set; }
        [MaxLength(20)]
        public string? LastName { get; set; }
        [MaxLength(10)]
        public string? FirstName { get; set; }
        
    }
}
