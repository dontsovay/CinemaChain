using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaChain.API.Controllers
{
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public CinemasController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        // GET: api/Cinemas
        [HttpGet]
        public IEnumerable<Cinemas> GetCinemas()
        {
            return _cinemaService.GetCinemas();
        }

        // GET: api/Cinemas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCinema([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cinemas = await Task.Run(() => _cinemaService.GetCinemasId(id));

            if (cinemas == null)
            {
                return NotFound();
            }

            return Ok(cinemas);
        }
        //[Authorize(Policy = "owner")]
        // PUT: api/Cinemas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinema([FromRoute] int id, [FromBody] CinemaUpdVM cinemas)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == 0 || id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _cinemaService.UpdateCinema(cinemas));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }

            return NoContent();
        }
        //[Authorize(Policy = "owner")]
        //[EnableCors]
        // POST: api/Cinemas
        [HttpPost]
        public IActionResult PostCinema([FromBody] Cinemas cinemas)
        {
            if (cinemas == null)
            {
                return BadRequest();
            }
            _cinemaService.CreateCinema(cinemas);

            return Ok(cinemas);

        }
       //  [Authorize(Policy = "owner")]
        //[EnableCors]
        // DELETE: api/Cinemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _cinemaService.DeleteCinema(id));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool CinemasExists(int id)
        {
            return _cinemaService.GetCinemasId(id) != null;
        }
    }
}