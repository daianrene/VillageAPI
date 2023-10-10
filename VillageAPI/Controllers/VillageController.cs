using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public VillageController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<VillageDto>>> GetAll()
        {
            var villageList = await _dbContext.Villages.ToListAsync();
            var villageListDTO = _mapper.Map<IEnumerable<VillageDto>>(villageList);

            return Ok(villageListDTO);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillageDto>> GetVillage(int id)
        {
            if (id < 1) return BadRequest();

            var Villa = await _dbContext.Villages.FirstOrDefaultAsync(x => x.Id == id);
            if (Villa == null) return NotFound();
            var VillaDTO = _mapper.Map<VillageDto>(Villa);

            return Ok(VillaDTO);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillageDto>> PostVillage([FromBody] VillageDto newVillageDTO)
        {
            if (!ModelState.IsValid || newVillageDTO == null)
            {
                return BadRequest(ModelState);
            }

            Village newVillage = _mapper.Map<Village>(newVillageDTO);

            await _dbContext.AddAsync(newVillage);
            await _dbContext.SaveChangesAsync();
            return NoContent();
            // return CreatedAtAction(nameof(GetVillage), new { id = newVillage.Id }, newVillage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVillage(int id, [FromBody] VillageDto villageUpdDTO)
        {
            if (id != villageUpdDTO.Id || !ModelState.IsValid) return BadRequest(ModelState);

            var result = await _dbContext.Villages.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) return NotFound();

            _mapper.Map(villageUpdDTO, result);
            result.UpdatedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {

            var village = await _dbContext.Villages.FirstOrDefaultAsync(v => v.Id == id);
            if (village == null) return NotFound();

            _dbContext.Villages.Remove(village);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
