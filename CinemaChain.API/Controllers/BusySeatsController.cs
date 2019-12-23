using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaChain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusySeatsController : ControllerBase
    {
        private readonly IBusySeatService _seatsBusyService;


        public BusySeatsController(IBusySeatService seatsBusyService)
        {
            _seatsBusyService = seatsBusyService;
        }


        // GET: api/BusySeats
        [HttpGet]
        public IEnumerable<BusySeats> GetBusySeats()
        {
            return _seatsBusyService.GetSeatsBusy();
        }


        // GET: api/BusySeats/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusySeat([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seats_Busy = await Task.Run(() => _seatsBusyService.GetSeatsBusyIdSeance(id));

            if (seats_Busy == null)
            {
                return NotFound();
            }

            return Ok(seats_Busy);
        }
    }
}
