using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class SeatsVM
    {
        public int SeatId { get; set; }
        public int SeatNumber { get; set; }
        public int CinemaId { get; set; }
        public SeatsVM(Seats seats)
        {
            SeatId = seats.Id;
            SeatNumber = seats.SeatNumber;
            CinemaId = seats.CinemaId;

        }
    }
}
