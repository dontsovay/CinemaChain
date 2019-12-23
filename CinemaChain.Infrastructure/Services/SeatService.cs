using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Infrastructure.Services
{
    public class SeatService: ISeatService
    {
        UnitOfWork unitOfWork;

        public SeatService(DB_Context context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Owners> GetSeats()
        {
            return unitOfWork.Owners.GetAll();
        }
        public void CreateSeat(int cinemaId, int seatNumber)
        {
            Seats seats = new Seats();
            seats.CinemaId = cinemaId;
            seats.SeatNumber = seatNumber;

            unitOfWork.Seats.Create(seats);
        }
    }
}
