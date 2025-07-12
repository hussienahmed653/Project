using System.ComponentModel.DataAnnotations;

namespace Project.Domain
{
    public class Region
    {
        public int RegionID { get; set; }
        [MaxLength(50)]
        public string RegionDescription { get; set; }
        public ICollection<Territorie> Territories { get; set; }
    }
}
