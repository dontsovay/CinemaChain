using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface ISeatService
    {
        IEnumerable<Owners> GetSeats();
        void CreateSeat(int cinemaId, int seatNumber);
    }
}
