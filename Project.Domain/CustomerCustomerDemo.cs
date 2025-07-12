using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain
{
    public class CustomerCustomerDemo
    {
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public string CustomerTypeID { get; set; }
        public CustomerDemographics CustomerDemographics { get; set; }
    }
}
