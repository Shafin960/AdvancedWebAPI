using AutoMapper;
using MagivVilla_VillaAPI.Data;
using MagivVilla_VillaAPI.DTO;
using MagivVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace MagivVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public VillaAPIController(
            ILogger<VillaAPIController> logger, 
            ApplicationDbContext context, 
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villas = await _context.Villas.ToListAsync();
            return  Ok(_mapper.Map<List<VillaDTO>>(villas));
        }

        [HttpGet("GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if(id ==0 ) return BadRequest();
            var villa = await _context.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if(villa == null) return NotFound();
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>>  CreateVilla([FromBody]VillaCreateDTO villaDto)
        {
            if(villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if ( _context.Villas.ToList().FirstOrDefault(u => u.Name == villaDto.Name) != null)
            {
                ModelState.AddModelError("CustomerError","Villa name is already taken");
                return BadRequest(ModelState);
            }

            Villa villa = _mapper.Map<Villa>(villaDto);
            await _context.Villas.AddAsync(villa);
            await _context.SaveChangesAsync();
            return Ok(villa);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa =await _context.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _context.Villas.Remove(villa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDto)
        {
            if (villaDto.Id != id || villaDto == null)
            {
                return BadRequest();    
            }

            Villa villa = _mapper.Map<Villa>(villaDto);
            villa.ImageUrl = villaDto.ImageUrl;
            _context.Villas.Update(villa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
