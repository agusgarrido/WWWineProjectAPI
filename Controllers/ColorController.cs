using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWWineProjectAPI.Data;
using WWWineProjectAPI.DTO;
using WWWineProjectAPI.Models;

namespace WWWineProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {

        private readonly WWWineProjectDb _db;

        public ColorController(WWWineProjectDb db)
        {
            _db = db;
        }

        // GET - Get All

        [HttpGet]
        public async Task<ActionResult<List<ColorDto>>> GetAllAsync()
        {
            var color = await _db.Colors
                .Include(c => c.Varieties)
                .Select(c => new ColorDto
                {
                    ColorID = c.ColorID,
                    Name = c.Name,
                    Varieties = c.Varieties
                        .Select(v => new ColorVarietyDto
                        {
                            VarietyID = v.VarietyID,
                            Name = v.Name
                        })
                        .ToList()
                })
                .ToListAsync();
            return Ok(color);
        }
    }
}
