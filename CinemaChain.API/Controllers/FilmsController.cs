using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaChain.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmsController(IFilmService filmService)
        {
            _filmService = filmService;
        }


        // GET: api/Films
        [HttpGet]
        public IEnumerable<Films> GetFilms()
        {
            return _filmService.GetFilms();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(ModelState);
            }

            var users = await Task.Run(() => _filmService.GetFilmsId(id));

            if (users == null)
            {
                return NotFound();
            }
            else return Ok(users);
        }
        //[Authorize(Policy = "admin")]
        // POST: api/Films
        [HttpPost]
        [EnableCors]
        public IActionResult PostFilm([FromBody] Films films)
        {

            if (films == null)
            {
                return BadRequest();
            }
            else
            {
                _filmService.CreateFilm(films);

                return Ok(films);
            }

            //CreatedAtAction("Get", new { id = films.id_film }, films);

        }

        //// PUT: api/Films/5
        //[Authorize(Policy = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilms([FromRoute] int id, [FromBody] Films films)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == 0)
            {
                return BadRequest();
            }


            try
            {
                await Task.Run(() => _filmService.UpdateFilm(films));

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(films);
        }

        //[Authorize(Policy = "admin")]
        [EnableCors]
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _filmService.DeleteFilm(id));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }

            return Ok(id);
        }
        private bool FilmsExists(int id)
        {
            return _filmService.GetFilmsId(id) != null;
        }
    }
}
