using System.ComponentModel.DataAnnotations;

namespace Project.Domain
{
    public class Categories
    {
        public int CategoryID { get; set; }
        [MaxLength(15)]
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<CategoryFile> CategoryFiles { get; set; } = new List<CategoryFile>();
    }
}
