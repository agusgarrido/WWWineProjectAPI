using System.ComponentModel.DataAnnotations.Schema;

namespace WWWineProjectAPI.Models
{
    public class VarietyRegion
    {
        public int VarietyRegionID { get; set; }
        [ForeignKey("Variety")]
        public int VarietyID { get; set; }
        public Variety Variety { get; set; }
        [ForeignKey("Region")]
        public int RegionID { get; set; }
        public Region Region { get; set; }
    }
}
