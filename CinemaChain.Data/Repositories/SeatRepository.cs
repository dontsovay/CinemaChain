using CinemaChain.Data.Context;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaChain.Data.Repositories
{
    public class SeatRepository : BaseRepository<Seats>, ISeatRepository
    {
        private DB_Context db;

        public SeatRepository(DB_Context context) : base(context)
        {
            this.db = context;
        }
        public override IEnumerable<Seats> GetAll()
        {
            return db.Seats;
        }

        public override Seats Get(int id)
        {
            return db.Seats.Find(id);
        }
        public override Seats Create(Seats seat)
        {
            db.Database.ExecuteSqlCommand("use CINEMABASE;" +
                "execute sp_InsertSeats " + seat.CinemaId + ", " + seat.SeatNumber);
            return seat;

        }

        public override Seats Update(Seats seat)
        {
            db.Entry(seat).State = EntityState.Modified;
            db.SaveChangesAsync();
            return seat;

        }

        public override void Delete(int id)
        {
            Seats seats = db.Seats.Where(i => i.Id.Equals(id)).FirstOrDefault();
            if (seats == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Seats.Remove(seats);
                db.SaveChanges();
            }
        }


    }
}
