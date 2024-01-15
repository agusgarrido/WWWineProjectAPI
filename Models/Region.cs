using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWWineProjectAPI.Models
{
    public class Region
    {
        public int RegionID { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public Country Country { get; set; }
        public ICollection<VarietyRegion> Varieties { get; set; }

    }
}
