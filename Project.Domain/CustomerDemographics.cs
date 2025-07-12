using System.ComponentModel.DataAnnotations;

namespace Project.Domain
{
    public class CustomerDemographics
    {
        [MaxLength(10)]
        public string CustomerTypeID { get; set; }
        public string? CustomerDesc { get; set; }
        public ICollection<CustomerCustomerDemo> customerCustomerDemos { get; set; } = new List<CustomerCustomerDemo>();
    }
}
