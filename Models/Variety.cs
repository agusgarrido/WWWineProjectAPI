using System.ComponentModel.DataAnnotations.Schema;

namespace WWWineProjectAPI.Models
{
    public class Variety
    {
        public int VarietyID { get; set; }
        public string Name { get; set; }
        [ForeignKey("Color")]
        public int ColorID { get; set; }
        public Color Color { get; set; }
        [ForeignKey("Country")]
        public int OriginID {  get; set; }
        public Country Origin { get; set; }
        public ICollection<VarietyRegion> Regions { get; set; }
        public ICollection<VarietyFlavor> Flavors { get; set; }

    }
}
