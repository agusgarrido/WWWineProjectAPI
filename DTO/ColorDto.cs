using WWWineProjectAPI.Models;

namespace WWWineProjectAPI.DTO
{
    public class ColorDto
    {
        public int ColorID { get; set; }
        public string Name { get; set; }
        public ICollection<ColorVarietyDto> Varieties { get; set; }
    }
}
