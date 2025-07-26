using Microsoft.AspNetCore.Http;
using Project.Domain;
using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class AddEmployeeDto : BaseEmployeeDto
    {
        public string LastName { get; set; }
        [MaxLength(10)]
        public string FirstName { get; set; }
    }
}
