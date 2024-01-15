namespace WWWineProjectAPI.DTO
{
    public class RegionInputDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class VarietyInputDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Origin { get; set; }
        public List<RegionInputDto> Regions { get; set; }
    }

    public class VarietyRegionUpdateDto
    {
        public string Name { get; set; }
        public List<RegionInputDto> Regions { get; set; }
    }

    public class VarietyUpdateDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Origin { get; set; }
    }
}
