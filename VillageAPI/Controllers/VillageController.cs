using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        protected APIResponse _response;
        public VillageController(IMapper mapper, IVillageRepository villageRepo)
        {
            _mapper = mapper;
            _villageRepo = villageRepo;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                var villageList = await _villageRepo.GetAllAsync();
                var villageListDTO = _mapper.Map<IEnumerable<VillageDto>>(villageList);

                _response.Result = villageListDTO;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors = new List<string> { ex.ToString() };
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillage(int id)
        {
            try
            {
                if (id < 1)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var Villa = await _villageRepo.GetAsync(x => x.Id == id);
                if (Villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                var VillaDTO = _mapper.Map<VillageDto>(Villa);

                _response.Result = VillaDTO;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.Errors = new List<string> { ex.ToString() };
                _response.IsSuccess = false;
                return _response;
            }

        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostVillage([FromBody] VillageDto newVillageDTO)
        {
            try
            {
                if (!ModelState.IsValid || newVillageDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                Village newVillage = _mapper.Map<Village>(newVillageDTO);
                newVillage.CreatedDate = DateTime.Now;
                newVillage.UpdatedDate = DateTime.Now;

                await _villageRepo.CreateAsync(newVillage);
                _response.Result = newVillage;
                _response.StatusCode = HttpStatusCode.OK;

                return NoContent();
                // return CreatedAtAction(nameof(GetVillage), new { id = newVillage.Id }, _response);

            }
            catch (Exception ex)
            {
                _response.Errors = new List<string> { ex.ToString() };
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVillage(int id, [FromBody] VillageDto villageUpdDTO)
        {
            try
            {
                if (id != villageUpdDTO.Id || !ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var result = await _villageRepo.GetAsync(x => x.Id == id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                _mapper.Map(villageUpdDTO, result);
                await _villageRepo.UpdateAsync(result);

                return NoContent();

            }
            catch (Exception ex)
            {
                _response.Errors = new List<string> { ex.ToString() };
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var village = await _villageRepo.GetAsync(v => v.Id == id);
                if (village == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                await _villageRepo.RemoveAsync(village);
                return NoContent();

            }
            catch (Exception ex)
            {
                _response.Errors = new List<string> { ex.ToString() };
                _response.IsSuccess = false;
                return BadRequest(_response);

            }
        }
    }
}
