using System.ComponentModel.DataAnnotations;

namespace Project.Domain.ViewClasses
{
    public class ViewProductData
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; } // has default value of 0
        public byte? UnitsInStock { get; set; } // has default value of 0
        public byte? UnitsOnOrder { get; set; } // has default value of 0
        public byte? ReorderLevel { get; set; } // has default value of 0
        public bool? Discontinued { get; set; } // has default value of 0
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? CategoryID { get; set; }
        public Guid CategoryGuid { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
        public int? SupplierID { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? HomePage { get; set; }
    }
}
