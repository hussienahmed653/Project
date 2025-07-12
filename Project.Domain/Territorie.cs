using System.ComponentModel.DataAnnotations;

namespace Project.Domain
{
    public class Territorie
    {
        public int TerritoryID { get; set; }
        [MaxLength(50)]
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
        public Region Region { get; set; }
    }
}
