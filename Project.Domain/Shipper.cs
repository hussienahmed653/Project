using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain
{
    public class Shipper
    {
        public int ShipperID { get; set; }
        [MaxLength(40)]
        public string CompanyName { get; set; }
        [MaxLength(24)]
        public string? Phone { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
