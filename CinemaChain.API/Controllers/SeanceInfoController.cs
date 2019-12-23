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
    public class SeanceInfoController : ControllerBase
    {
        // GET: api/SeanceInfo
        private readonly ISeanceService _seanceService;
        private readonly IFilmService _filmService;
        private readonly IBusySeatService _seatsBusyService;
        private readonly ICinemaService _cinemaService;

        public SeanceInfoController(ISeanceService seanceService, IFilmService filmService, IBusySeatService seatsBusyService, ICinemaService cinemaService)
        {
            _seanceService = seanceService;
            _filmService = filmService;
            _seatsBusyService = seatsBusyService;
            _cinemaService = cinemaService;
        }


        // GET: api/Seance_Info
        //[HttpGet]
        //public IEnumerable<Seances> GetSeances()
        //{
        //    return _context.Seances;
        //}

        // GET: api/Seance_Info/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeances([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var SeatId = await Task.Run(() => _seatsBusyService.GetSeat(id));
            var seances = await Task.Run(() => _seanceService.GetInfoAboutSeanceandFilm(id));
            if (seances == null)
            {
                return NotFound();
            }
            var namecinema = _cinemaService.GetCinemaNameId(seances.CinemaId).CinemaName;
            var namefilm = _filmService.GetFilmsId(seances.FilmId).FilmName;
            if (namefilm == null)
            {
                return NotFound();
            }
            if (namecinema == null)
            {
                return NotFound();
            }
            SeanceInfoVM seanceInfo = new SeanceInfoVM();
            seanceInfo.SeatNumber = SeatId;
            seanceInfo.SeanceId = seances.Id;
            seanceInfo.FilmName = namefilm;
            seanceInfo.CinemaName = namecinema;
            seanceInfo.SeanceDate = seances.SeanceDate;
            seanceInfo.Price = seances.Price;
            return Ok(seanceInfo);
        }

        // PUT: api/Seance_Info/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSeances([FromRoute] int id, [FromBody] Seances seances)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != seances.Id_Seance)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(seances).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SeancesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [EnableCors]
        // POST: api/Seance_Info
        [HttpPost]
        public async Task<int> GetBusyById([FromBody] BusyVM busyModel)
        {

            var data = await Task.Run(() => _seatsBusyService.GetBusy(busyModel.SeanceId, busyModel.SeatNumber));

            return data;
        }

        //// DELETE: api/Seance_Info/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSeances([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var seances = await _context.Seances.FindAsync(id);
        //    if (seances == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Seances.Remove(seances);
        //    await _context.SaveChangesAsync();

        //    return Ok(seances);
        //}

        //private bool SeancesExists(int id)
        //{
        //    return _context.Seances.Any(e => e.Id_Seance == id);
        //}
    }
}
