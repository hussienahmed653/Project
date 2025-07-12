using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain
{
    public class Order
    {
        public int OrderID { get; set; }
        public int? CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int? EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public Shipper Shipper { get; set; }
        public decimal? Freight { get; set; }
        [MaxLength(40)]
        public string? ShipName { get; set; }
        [MaxLength(60)]
        public string? ShipAddress { get; set; }
        [MaxLength(15)]
        public string? ShipCity { get; set; }
        [MaxLength(15)]
        public string? ShipRegion { get; set; }
        [MaxLength(10)]
        public string? ShipPostalCode { get; set; }
        [MaxLength(15)]
        public string? ShipCountry { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
