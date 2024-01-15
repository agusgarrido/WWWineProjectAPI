using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;
using WWWineProjectAPI.Models;

namespace WWWineProjectAPI.DTO
{
    public class VarietyDto
    {
        public int VarietyID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Origin { get; set; }
    }

    public class ColorVarietyDto
    {
        public int VarietyID { get; set; }
        public string Name { get; set; }
    }
    public class VarietyRegionInfoDto
    {
        public int VarietyID { get; set; }
        public string Name { get; set; }
        public ICollection<VarietyRegionDto> Regions { get; set; }
    }

    public class VarietyNameDto
    {
        public int VarietyID { get; set; }
        public string Name { get; set; }
    }
}
