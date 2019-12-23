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
using Microsoft.EntityFrameworkCore;

namespace CinemaChain.API.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MyOrdersController : ControllerBase
    {
        // GET: api/MyOrders
        private readonly IOrderService _orderService;
        private readonly ICinemaService _cinemaService;
        private readonly IFilmService _filmService;
        private readonly ISeanceService _seanceService;

        public MyOrdersController(IOrderService orderService, ICinemaService cinemaService, IFilmService filmService, ISeanceService seanceService)
        {
            _orderService = orderService;
            _filmService = filmService;
            _cinemaService = cinemaService;
            _seanceService = seanceService;
        }

        // GET: api/MyOrders
        [HttpGet]
        public IEnumerable<Orders> GetOrders()
        {
            return _orderService.GetOrders();
        }



        // GET: api/Orders/5
        [HttpGet("{username}")]
        public IEnumerable<OrdersAllVM> GetOrders([FromRoute] string username)
        {

            IEnumerable<Orders> orders = _orderService.PostOrders(username);
            List<OrdersAllVM> orderall = new List<OrdersAllVM>();
            foreach (Orders order in orders)
            {
                var date = _seanceService.GetSeanceId(order.SeanceId).SeanceDate;
                var price = _seanceService.GetSeanceId(order.SeanceId).Price;
                var idcinema = _seanceService.GetSeanceId(order.SeanceId).CinemaId;
                var idfilm = _seanceService.GetSeanceId(order.SeanceId).FilmId;
                var namecinema = _cinemaService.GetCinemaNameId(idcinema).CinemaName;
                var namefilm = _filmService.GetFilmsId(idfilm).FilmName;

                OrdersAllVM ordersAll = new OrdersAllVM();
                ordersAll.OrderId = order.Id;
                ordersAll.CinemaName = namecinema;
                ordersAll.FilmName = namefilm;
                ordersAll.SeatNumber = order.SeatNumber;
                ordersAll.SeanceDate = date;
                ordersAll.IsPaid = order.IsPaid;
                ordersAll.Price = price;
                ordersAll.SeanceId = order.SeanceId;

                orderall.Add(ordersAll);

            }

            return orderall;
        }

        // PUT: api/Orders/5

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
               await Task.Run(()=> _orderService.UpdateOrder(id));

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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
        /*
         *  [HttpGet("{username}")]
        public async Task<IActionResult> GetOrderName([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orders = await Task.Run(() => _orderService.postOrders(username));

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }*/
        // POST: api/MyOrders

        [EnableCors]
        [HttpPost]
        public async Task<IActionResult> PostOrders([FromBody] UsernameVM usernameModel)
        {

            var orders = await Task.Run(() => _orderService.PostOrders(usernameModel.Username));

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [EnableCors]
        // DELETE: api/MyOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orders = _orderService.GetOrdersId(id);
            if (orders == null)
            {
                return NotFound();
            }
            await Task.Run(() =>
            _orderService.DeleteOrder(id));

            return Ok(orders);
        }

        private bool OrdersExists(int id)
        {
            return _orderService.GetOrdersId(id) != null;
        }

    }
}
