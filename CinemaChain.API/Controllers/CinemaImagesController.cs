using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaChain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaImagesController : ControllerBase
    {
        private readonly ICinemaImageService _cinemaImageService;

        public CinemaImagesController(ICinemaImageService cinemaImageService)
        {
            _cinemaImageService = cinemaImageService;
        }

        // GET: api/CinemaImages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImagee([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(ModelState);
            }

            var users = await Task.Run(() => _cinemaImageService.GetCinemaImage(id));

            if (users == null)
            {
                return NotFound();
            }
            else return Ok(users);
        }

        // POST: api/CinemaImages
        [HttpPost]
        [EnableCors]
        public IActionResult PostCinemaImages([FromBody] CIWM cinemaim)
        {

            if (cinemaim == null)
            {
                return BadRequest();
            }
            else
            {
                var image = _cinemaImageService.CreateImage(cinemaim);

                return Ok(image);
            }
        }

        // PUT: api/CinemaImages/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
