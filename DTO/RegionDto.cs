using WWWineProjectAPI.Models;

namespace WWWineProjectAPI.DTO
{
    public class RegionDto
    {
        public int RegionID { get; set; }
        public string Name { get; set; }

    }

    public class RegionVarietyInfoDto
    {
        public int RegionID { get; set; }
        public string Name { get; set; }
        public ICollection<RegionVarietyDto> Varieties { get; set; }
    }
}
