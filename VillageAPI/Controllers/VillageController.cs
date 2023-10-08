using Microsoft.AspNetCore.Mvc;
using VillageAPI.DataAccess;
using VillageAPI.Models;
using VillageAPI.Models.Dto;

namespace VillageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillageController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public VillageController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillageDto>> GetAll()
        {
            return Ok(_dbContext.Villages.ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillageDto> GetVillage(int id)
        {
            if (id < 1) return BadRequest();

            var Villa = _dbContext.Villages.FirstOrDefault(x => x.Id == id);
            if (Villa == null) return NotFound();

            return Ok(Villa);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillageDto> PostVillage([FromBody] VillageDto newVillageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (newVillageDTO == null) return BadRequest();

            Village newVillage = new()
            {
                Name = newVillageDTO.Name,
                Description = newVillageDTO.Description,
                Population = newVillageDTO.Population,
                M2 = newVillageDTO.M2,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,

            };

            _dbContext.Add(newVillage);
            _dbContext.SaveChanges();
            return NoContent();
            // return CreatedAtAction(nameof(GetVillage), new { id = newVillage.Id }, newVillage);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVillage(int id, [FromBody] VillageDto villageDTO)
        {
            if (id != villageDTO.Id) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _dbContext.Villages.FirstOrDefault(x => x.Id == id);
            if (result == null) return NotFound();

            result.Name = villageDTO.Name;
            result.Description = villageDTO.Description;
            result.Population = villageDTO.Population;
            result.M2 = villageDTO.M2;
            result.UpdatedDate = DateTime.Now;

            //No working
            _dbContext.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {

            var village = _dbContext.Villages.FirstOrDefault(v => v.Id == id);
            if (village == null) return NotFound();

            _dbContext.Villages.Remove(village);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
