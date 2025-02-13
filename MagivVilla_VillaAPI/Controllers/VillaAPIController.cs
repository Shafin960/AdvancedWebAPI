using MagivVilla_VillaAPI.DTO;
using MagivVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagivVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(new List<VillaDTO> 
            {
                new VillaDTO{Id = 1 , Name="Pool View"},
                new VillaDTO{Id = 2 , Name="Beach View"}
            });
        }

        //[HttpGet("id")]
        //public VillaDTO GetVilla(int id)
        //{

        //}

        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            if(villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return new VillaDTO
            {
                Id = 1,
                Name = "Sajek"
            };
        }
    }
}
