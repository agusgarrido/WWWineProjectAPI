using System.ComponentModel.DataAnnotations;

namespace WWWineProjectAPI.Models
{
    public class Color
    {
        public int ColorID { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Variety> Varieties { get; set; }
    }
}
