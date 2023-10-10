using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VillageAPI.Models;
using VillageAPI.Models.Dto;
using VillageAPI.Repository.IRepository;

namespace VillageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVillageRepository _villageRepo;
        public VillageController(IMapper mapper, IVillageRepository villageRepo)
        {
            _mapper = mapper;
            _villageRepo = villageRepo;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<VillageDto>>> GetAll()
        {
            var villageList = await _villageRepo.GetAllAsync();
            var villageListDTO = _mapper.Map<IEnumerable<VillageDto>>(villageList);

            return Ok(villageListDTO);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillageDto>> GetVillage(int id)
        {
            if (id < 1) return BadRequest();

            var Villa = await _villageRepo.GetAsync(x => x.Id == id);
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

            await _villageRepo.CreateAsync(newVillage);
            return NoContent();
            // return CreatedAtAction(nameof(GetVillage), new { id = newVillage.Id }, newVillage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVillage(int id, [FromBody] VillageDto villageUpdDTO)
        {
            if (id != villageUpdDTO.Id || !ModelState.IsValid) return BadRequest(ModelState);

            var result = await _villageRepo.GetAsync(x => x.Id == id);
            if (result == null) return NotFound();

            _mapper.Map(villageUpdDTO, result);
            await _villageRepo.UpdateAsync(result);

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {

            var village = await _villageRepo.GetAsync(v => v.Id == id);
            if (village == null) return NotFound();

            await _villageRepo.RemoveAsync(village);
            return NoContent();
        }
    }
}
