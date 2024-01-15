using System.ComponentModel.DataAnnotations;

namespace WWWineProjectAPI.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Variety> Varieties { get; set; }
    }
}
