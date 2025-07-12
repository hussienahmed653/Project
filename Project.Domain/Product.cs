using System.ComponentModel.DataAnnotations;

namespace Project.Domain
{
    public class Product
    {
        public int ProductID { get; set; }
        [MaxLength(40)]
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public Supplier supplier { get; set; }
        public int? CategoryID { get; set; }
        public Categories category { get; set; }
        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; } // has default value of 0
        public byte? UnitsInStock { get; set; } // has default value of 0
        public byte? UnitsOnOrder { get; set; } // has default value of 0
        public byte? ReorderLevel { get; set; } // has default value of 0
        public bool Discontinued { get; set; } // has default value of 0
    }
}
