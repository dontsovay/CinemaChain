using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class OrdersAllVM
    {
        public int OrderId { get; set; }
        public int SeanceId { get; set; }
        public string CinemaName { get; set; }
        public string FilmName { get; set; }
        public DateTime SeanceDate { get; set; }
        public int SeatNumber { get; set; }
        public int Price { get; set; }
        public bool IsPaid { get; set; }
    }
}
