using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WWWineProjectAPI.Data;
using WWWineProjectAPI.DTO;
using WWWineProjectAPI.Models;

namespace WWWineProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VarietyController : ControllerBase
    {
        private readonly WWWineProjectDb _db;

        public VarietyController(WWWineProjectDb db)
        {
            _db = db;
        }

        // GET - Get All

        [HttpGet("get-all")]
        public async Task<ActionResult<List<VarietyDto>>> GetAllAsync()
        {
            var varieties = await _db.Varieties
                .Select(v => new VarietyDto
                {
                    VarietyID = v.VarietyID,
                    Name = v.Name,
                    Color = v.Color.Name,
                    Origin = v.Origin.Name
                })
                .ToListAsync();

            return Ok(varieties);
        }

        // GET - Get by ID

        [HttpGet("get-id/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var variety = await _db.Varieties
                .Where(v => v.VarietyID == id)
                .Select(v => new VarietyDto
                {
                    VarietyID = v.VarietyID,
                    Name = v.Name,
                    Color = v.Color.Name,
                    Origin = v.Origin.Name
                })
                .FirstOrDefaultAsync();

            if(variety is null)
            {
                return NotFound("Variety doesn't exist");
            }

            return Ok(variety);
        }

        // GET - Get by Name

        [HttpGet("get-name/{varietyName}")]
        public async Task<ActionResult> GetByNameAsync(string varietyName)
        {
            var formattedName = varietyName.ToLower().Replace(" ", "-");
            var variety = await _db.Varieties
                .Include(v => v.Regions)
                .Where(v => v.Name.ToLower().Replace(" ", "-") == formattedName)
                .Select(v => new VarietyDto
                {
                    VarietyID = v.VarietyID,
                    Name = v.Name,
                    Color = v.Color.Name,
                    Origin = v.Origin.Name,
                })
                .FirstOrDefaultAsync();

            if (variety is null)
            {
                return NotFound("Variety doesn't exist");
            }

            return Ok(variety);
        }

        // GET - Get regions by variety

        [HttpGet("get-regions/{varietyName}")]
        public async Task<ActionResult> GetRegionsByVarietyAsync(string varietyName)
        {
            var formattedName = varietyName.ToLower().Replace(" ", "").Replace("-", "");
            var variety = await _db.Varieties
                .Where(v => v.Name.ToLower().Replace(" ", "") == formattedName)
                .Select(v => new VarietyRegionInfoDto
                {
                    VarietyID = v.VarietyID,
                    Name = v.Name,
                    Regions = v.Regions
                        .Select(r => new VarietyRegionDto
                        {
                            RegionID = r.RegionID,
                            Region = r.Region.Name
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (variety is null)
                {
                    return NotFound("Variety doesn't exist");
                }

            return Ok(variety);
        }

        // POST - Add variety

        [HttpPost("add")]
        public async Task<ActionResult<VarietyDto>> AddVariety([FromBody] VarietyInputDto varietyInput)
        {
            if (await VarietyExists(varietyInput.Name))
            {
                return BadRequest("Variety already exists");
            }

            var color = await _db.Colors.FirstOrDefaultAsync(c => c.Name.ToLower() == varietyInput.Color.ToLower());
            if (color is null)
            {
                return BadRequest(@"Invalid color. Try with ""Tinta"" or ""Blanca""");
            }

            var country = await GetOrCreateCountry(varietyInput.Origin);

            var newVariety = new Variety
            {
                Name = varietyInput.Name,
                ColorID = color.ColorID,
                Origin = country
            };

            _db.Varieties.Add(newVariety);
            await _db.SaveChangesAsync();

            await HandleRegions(varietyInput.Regions, newVariety.VarietyID);

            var varietyDto = new VarietyDto
            {
                VarietyID = newVariety.VarietyID,
                Name = newVariety.Name,
                Color = newVariety.Color.Name,
                Origin = newVariety.Origin.Name,
            };

            return Ok(varietyDto);
        }

        private async Task<bool> VarietyExists(string varietyName){
            return await _db.Varieties
                .AnyAsync(v => v.Name.ToLower().Replace(" ", "") == varietyName.ToLower().Replace(" ", ""));
        }

        private async Task<Country> GetOrCreateCountry(string countryName)
        {
            var country = await _db.Countries
                .FirstOrDefaultAsync(c => c.Name.ToLower() == countryName.ToLower());

            if (country is null)
            {
                country = new Country { Name = countryName };
                _db.Countries.Add(country);
                await _db.SaveChangesAsync();
            }

            return country;
        }

        private async Task<Region> GetOrCreateRegion(Country country, string regionName)
        {
            var existingRegion = await _db.Regions
                .FirstOrDefaultAsync(r => r.Name.ToLower().Replace(" ", "") == regionName.ToLower().Replace(" ", "") && r.CountryID == country.CountryID);

            if (existingRegion is null)
            {
                var newRegion = new Region { Name = regionName, CountryID = country.CountryID };
                _db.Regions.Add(newRegion);
                await _db.SaveChangesAsync();
                return newRegion;
            }

            return existingRegion;
        }

        private async Task HandleRegions(List<RegionInputDto> regions, int varietyId)
        {
            foreach (var region in regions)
            {
                var existingCountry = await GetOrCreateCountry(region.Country);
                var existingRegion = await GetOrCreateRegion(existingCountry, region.Name);

                var varietyRegion = new VarietyRegion { VarietyID = varietyId, RegionID = existingRegion.RegionID };
                _db.VarietyRegions.Add(varietyRegion);
            }

            await _db.SaveChangesAsync();
        }

        // UPDATE - Update variety
        [HttpPut("update")]
        public async Task<ActionResult<VarietyDto>> UpdateVariety(VarietyUpdateDto varietyUpdate)
        {
            var variety = await _db.Varieties.FirstOrDefaultAsync(v => v.Name.ToLower().Replace(" ", "") == varietyUpdate.Name.ToLower().Replace(" ", ""));
            if (variety is null) return NotFound("Variety doesn't exist");

            variety.Name = varietyUpdate.Name;
            await _db.SaveChangesAsync();

            var color = await _db.Colors.FirstOrDefaultAsync(c => c.Name.ToLower() == varietyUpdate.Color.ToLower());
            if (color is null) return BadRequest(@"Invalid color. Try with ""Tinta"" or ""Blanca""");

            var country = await GetOrCreateCountry(varietyUpdate.Origin);

            var updatedVariety = new VarietyDto
            {
                VarietyID = variety.VarietyID,
                Name = variety.Name,
                Color = variety.Color.Name,
                Origin = variety.Origin.Name
            };

            return Ok(updatedVariety);
        }

        // DELETE - Delete by Name

        [HttpDelete("delete/{varietyName}")]
        public async Task<ActionResult> DeleteVariety(string varietyName)
        {
            var variety = await _db.Varieties.FirstOrDefaultAsync(v => v.Name.ToLower().Replace(" ", "") == varietyName.ToLower().Replace(" ", ""));
            if (variety is null) return NotFound("Variety doesn't exist");
           
            var varietyID = variety.VarietyID;
            var varietyRegion = _db.VarietyRegions
                .Where(v => v.VarietyID == varietyID);
            
            _db.VarietyRegions.RemoveRange(varietyRegion);
            await _db.SaveChangesAsync();

            _db.Varieties.Remove(variety);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
