using System.ComponentModel.DataAnnotations.Schema;

namespace WWWineProjectAPI.Models
{
    public class VarietyFlavor
    {
        public int VarietyFlavorID {  get; set; }
        [ForeignKey("Variety")]
        public int VarietyID { get; set; }
        public Variety Variety {  get; set; }
        [ForeignKey("Flavor")]
        public int FlavorID { get; set; }
        public Flavor Flavor { get; set; }

    }
}
