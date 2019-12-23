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
    public class SeancesController : ControllerBase
    {
        private readonly ISeanceService _seanceService;
        private readonly ICinemaService _cinemaService;
        private readonly IFilmService _filmService;

        public SeancesController(ISeanceService seanceService, ICinemaService cinemaService, IFilmService filmService)
        {
            _seanceService = seanceService;
            _filmService = filmService;
            _cinemaService = cinemaService;
        }

        // GET: api/Seances
        [HttpGet]
        public IEnumerable<SeanceAllVM> GetSeances()
        {
            IEnumerable<Seances> seances = _seanceService.GetSeances();
            List<SeanceAllVM> seanceall = new List<SeanceAllVM>();
            foreach (Seances seance in seances)
            {
                //var ss = _seanceService.getInfoAboutSeanceandFilm(seance.Id_Seance);

                var namecinema = _cinemaService.GetCinemaNameId(seance.CinemaId).CinemaName;
                var namefilm = _filmService.GetFilmsId(seance.FilmId).FilmName;

                SeanceAllVM seanceAll = new SeanceAllVM();
                seanceAll.SeanceId = seance.Id;
                seanceAll.FilmId = seance.FilmId;
                seanceAll.FilmName = namefilm;
                seanceAll.CinemaId = seance.CinemaId;
                seanceAll.CinemaName = namecinema;  
                seanceAll.SeanceDate = seance.SeanceDate;
                seanceAll.AllSeats = seance.AllSeats;
                seanceAll.CountSeats = seance.CountSeats;
                seanceAll.Price = seance.Price;
                seanceall.Add(seanceAll);
            }
            return seanceall;

        }
        // GET: api/Seances/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seances = await Task.Run(() => _seanceService.GetSeanceId(id));

            if (seances == null)
            {
                return NotFound();
            }

            return Ok(seances);
        }

        [EnableCors]
        // PUT: api/Seances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeance([FromRoute] int id, [FromBody] Seances seances)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                await Task.Run(() => _seanceService.UpdateSeance(seances));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeancesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [EnableCors]
        // POST: api/Seances
        
        [HttpPost]
        public async Task<IActionResult> PostSeances([FromBody] SeancesVM seances) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => _seanceService.CreateSeance(seances));

            return Ok(seances);

            //
        }
        
        [EnableCors]
        // DELETE: api/Seances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeances([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seances = _seanceService.GetSeanceId(id);
            if (seances == null)
            {
                return NotFound();
            }

            await Task.Run(() => _seanceService.DeleteSeance(id));

            return Ok(seances);
        }

        private bool SeancesExists(int id)
        {
            return _seanceService.GetSeanceId(id) != null;
        }
    }
}

