using System.ComponentModel.DataAnnotations;

namespace WWWineProjectAPI.DTO
{
    public class CountryDto
    {
        public int CountryID { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class CountryVarietiesDto
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public ICollection<VarietyNameDto> Varieties { get; set; }
    }
}
