using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaChain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmImagesController : ControllerBase
    {
        private readonly IFilmImageService _filmImageService;

        public FilmImagesController(IFilmImageService filmImageService)
        {
            _filmImageService = filmImageService;
        }


        //[HttpGet]
        //public IEnumerable<byte[]> GetFilmImages()
        //{
        //    return _filmImageService.GetFilmImages();
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(ModelState);
            }

            var users = await Task.Run(() => _filmImageService.GetFilmImage(id));

            if (users == null)
            {
                return NotFound();
            }
            else return Ok(users);
        }
        // POST: api/FilmImages
        [HttpPost]
        [EnableCors]
        public IActionResult PostFilmImages([FromBody] FIWM filmim)
        {

            if (filmim == null)
            {
                return BadRequest();
            }
            else
            {
                var image = _filmImageService.CreateImage(filmim);

                return Ok(image);
            }

            //CreatedAtAction("Get", new { id = films.id_film }, films);

        }

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilms([FromBody] FilmImages image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Task.Run(() => _filmImageService.UpdateImage(image));



            return Ok(image);
        
        }


        //[EnableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _filmImageService.DeleteFilmImage(id));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }

            return Ok(id);
        }
        private bool FilmImageExists(int id)
        {
            return _filmImageService.GetFilmImage(id) != null;
        }
    }
}
