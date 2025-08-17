using Project.Domain;

namespace Project.Application.DTOs
{
    public class ProductResponseDto
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; } // has default value of 0
        public byte? UnitsInStock { get; set; } // has default value of 0
        public byte? UnitsOnOrder { get; set; } // has default value of 0
        public byte? ReorderLevel { get; set; } // has default value of 0
        public bool? Discontinued { get; set; } // has default value of 0
        public List<Categories> Categories { get; set; } = new List<Categories>();
        public List<Supplier> Suppliers { get; set; } = new List<Supplier>();
    }
}
