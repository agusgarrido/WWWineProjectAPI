using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWWineProjectAPI.Data;
using WWWineProjectAPI.DTO;

namespace WWWineProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly WWWineProjectDb _db;
        public CountryController(WWWineProjectDb db)
        {
            _db = db;
        }

        // GET - Get All

        [HttpGet]
        public async Task<ActionResult<List<CountryDto>>> GetAllAsync()
        {
            var countries = await _db.Countries
                .Select(c => new CountryDto
                {
                    CountryID = c.CountryID,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(countries);
        }

        // GET - Get by Name

        [HttpGet("get-varieties/{countryName}")]
        public async Task<ActionResult<CountryDto>> GetCountryByName(string countryName)
        {
            var formattedCountryName = countryName.ToLower().Replace(" ", "-");
            var country = await _db.Countries
                .Where(c => c.Name.ToLower().Replace(" ", "-") == formattedCountryName)
                .Select(c => new CountryVarietiesDto
                {
                    CountryID = c.CountryID,
                    Name = c.Name,
                    Varieties = c.Varieties
                        .Select(v => new VarietyNameDto
                        {
                            VarietyID = v.VarietyID,
                            Name = v.Name
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (country is null)
            {
                return NotFound("Country doesn't exist");
            }

            return Ok(country);
        }

    }
}
