namespace WWWineProjectAPI.Models
{
    public class Flavor
    {
        public int FlavorID { get; set; }
        public string Name { get; set; }
        public ICollection<VarietyFlavor> Varieties { get; set; }
    }
}
