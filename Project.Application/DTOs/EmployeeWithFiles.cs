using Project.Domain;

namespace Project.Application.DTOs
{
    public class EmployeeWithFiles
    {
        public Domain.Employee Employee { get; set; }
        public List<FilePath> EntityFiles { get; set; }
    }
}
