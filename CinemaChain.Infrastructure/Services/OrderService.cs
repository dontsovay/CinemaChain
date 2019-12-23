using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Services
{
    public class OrderService: IOrderService
    {
        private UnitOfWork unitOfWork;
        private readonly IUserService _userService;
        private readonly IBusySeatService _seatsBusyService;
        private readonly ISeanceService _seanceService;

        public OrderService(IUserService userService, IBusySeatService seatsBusyService, ISeanceService seanceService, DB_Context context)
        {
            _userService = userService;
            _seatsBusyService = seatsBusyService;
            _seanceService = seanceService;
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Orders> GetSeance(int seanceId)
        {
            return unitOfWork.Orders.GetSeance(seanceId);
        }
        public IEnumerable<Orders> PostOrders(string username)
        {
            return unitOfWork.Orders.GetAll().Where(u => u.UserName.Equals(username));
        }

        public IEnumerable<Orders> GetOrders()
        {
            return unitOfWork.Orders.GetAll();
        }
        public Orders GetOrdersId(int orderId)
        {
            return unitOfWork.Orders.Get(orderId);
        }
        public bool IsPaid(int orderId)
        {
            var datas = unitOfWork.Orders.GetAll().Where(id => id.Id.Equals(orderId)).FirstOrDefault();

            if (datas.IsPaid == true)
            {
                return true;
            }
            else return false;
        }


        public Orders UpdateOrder(int id)
        {

            var datas = unitOfWork.Orders.GetAll().Where(i => i.Id.Equals(id)).FirstOrDefault();
            datas.IsPaid = true;
            return unitOfWork.Orders.Update(datas);
        }

        public void DeleteOrder(int id)
        {
            var seanceid = unitOfWork.Orders.GetAll().Where(i => i.Id.Equals(id)).FirstOrDefault().SeanceId;
            var seatnumber = unitOfWork.Orders.GetAll().Where(i => i.Id.Equals(id)).FirstOrDefault().SeatNumber;
            unitOfWork.Orders.Delete(id);
            
            _seatsBusyService.UpdateSeatBusyDelete(seanceid, seatnumber, false);
        }
        public void CreateOrder(Orders orders)
        {
            Orders order = new Orders();

            if (_seanceService.CountSeats(orders.SeanceId) != 0)
            {
                if (_seatsBusyService.IsSeatBusy(orders.SeanceId, orders.SeatNumber) == false)
                {
                    order.UserName = orders.UserName;
                    order.SeanceId = orders.SeanceId;
                    order.SeatNumber = orders.SeatNumber;
                    order.IsPaid = orders.IsPaid;
                   
                    unitOfWork.Orders.Create(order);
                    var id=unitOfWork.Orders.GetAll().Where(us => us.UserName.Equals(orders.UserName)).Where(si => si.SeanceId.Equals(orders.SeanceId)).Where(sei => sei.SeatNumber.Equals(orders.SeatNumber)).FirstOrDefault();
                    if (id != null)
                    { 
                        var count = _seanceService.GetSeanceId(order.SeanceId).CountSeats;
                        var newcount = _seanceService.SeatDecrement(orders.SeanceId);
                        if (newcount < count)
                        {
                            _seatsBusyService.UpdateSeatBusy(orders.SeanceId, orders.SeatNumber, true);
                        }
                    }
                    
                }
            }
            else Console.WriteLine("Is Busy");

        }
    }
}
