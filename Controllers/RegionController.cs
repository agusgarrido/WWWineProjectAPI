using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WWWineProjectAPI.Data;
using WWWineProjectAPI.DTO;

namespace WWWineProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly WWWineProjectDb _db;

        public RegionController(WWWineProjectDb db)
        {
            _db = db;
        }

        // GET - Get All

        [HttpGet]
        public async Task<ActionResult<List<RegionDto>>> GetAllAsync()
        {
            var regions = await _db.Regions
                .Select(r => new RegionDto
                {
                    RegionID = r.RegionID,
                    Name = r.Name
                })
                .ToListAsync();

            return Ok(regions);
        }

        // GET - Get by Name

        [HttpGet("get-variety/{regionName}")]
        public async Task<ActionResult<RegionDto>> GetVarietiesByRegion(string regionName)
        {
            var formmatedRegionName = regionName.ToLower().Replace(" ", "");
            var region = await _db.Regions
                .Where(r => r.Name.ToLower().Replace(" ", "") == formmatedRegionName)
                .Select(r => new RegionVarietyInfoDto
                {
                    RegionID = r.RegionID,
                    Name = r.Name,
                    Varieties = r.Varieties
                        .Select(v => new RegionVarietyDto
                        {
                            VarietyID = v.VarietyID,
                            Variety = v.Variety.Name
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (region is null)
            {
                return NotFound("Region doesn't exist");
            }

            return Ok(region);
        }
    }
}
