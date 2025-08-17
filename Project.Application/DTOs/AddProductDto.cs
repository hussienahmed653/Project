using Project.Domain;
using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs
{
    public class AddProductDto
    {
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; } 
        public byte? UnitsInStock { get; set; } 
        public byte? UnitsOnOrder { get; set; } 
        public byte? ReorderLevel { get; set; } 
        public bool Discontinued { get; set; } 

    }
}
