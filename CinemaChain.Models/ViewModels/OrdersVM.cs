using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    class OrdersVM
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public int SeanceId { get; set; }
        public int SeatId { get; set; }
        public bool IsPaid { get; set; }
        public OrdersVM(Orders orders)
        {
            OrderId = orders.Id;
            Username = orders.UserName;
            SeanceId = orders.SeanceId;
            SeatId = orders.SeatNumber;
            IsPaid = orders.IsPaid;
        }
    }
}
