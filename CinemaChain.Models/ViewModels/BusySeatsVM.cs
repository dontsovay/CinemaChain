using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class BusySeatsVM
    {
        public int SeatId { get; set; }
        public int SeanceId { get; set; }
        public int SeatNumber { get; set; }

        public BusySeatsVM(BusySeats busySeats)
        {
            SeatId = busySeats.Id;
            SeanceId = busySeats.SeanceId;
            SeatNumber = busySeats.SeatNumber;
        }
     
    }
}
