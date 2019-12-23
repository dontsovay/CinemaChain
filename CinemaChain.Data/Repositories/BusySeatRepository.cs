using CinemaChain.Data.Context;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Repositories
{
    public class BusySeatRepository : BaseRepository<BusySeats>, IBusySeatRepository
    {
        private DB_Context db;

        public BusySeatRepository(DB_Context context) : base(context)
        {
            this.db = context;
        }

        //public async Task<IEnumerable<BusySeats>> GetAll()
        //{
        //    return await db.Set<BusySeats>().ToListAsync();
        //}

        //public async Task<BusySeats> Get(int id)
        //{
        //    return await db.Set<BusySeats>().FindAsync(id);
        //}
        public override BusySeats Create(BusySeats busySeats)
        {
            db.Database.ExecuteSqlCommand("use CINEMABASE;" +
                "execute sp_InsertBusySeats " + busySeats.SeanceId + ", " + busySeats.SeatNumber);
            return busySeats;

        }

        public override BusySeats Update(BusySeats bs)
        {
            db.Entry(bs).Property(c => c.IsBusy).IsModified = true;
            db.SaveChanges();
            return bs;
        }

        //public async Task Delete(int id)
        //{
        //   BusySeats bs = db.BusySeats.Where(i => i.SeatBId.Equals(id)).FirstOrDefault();
        //    if (bs == null)
        //    {
        //        throw new Exception("Нечего удалять");

        //    }
        //    else
        //    {
        //        db.BusySeats.Remove(bs);

        //    }
        //    await db.SaveChangesAsync();
        //}
        public async Task<BusySeats> GetIdBusy(int busyId)
        {
            return await db.BusySeats.Where(i => i.Id.Equals(busyId)).FirstOrDefaultAsync();
        }
        public IEnumerable<BusySeats> GetBySeanceId(int seanceId)
        {
            return db.BusySeats.Where(id => id.SeanceId.Equals(seanceId));
        }

        public BusySeats GetId(int seanceId, int seatId)
        {
            return db.BusySeats.Where(i => i.SeanceId.Equals(seanceId)).Where(ii => ii.SeatNumber.Equals(seatId)).FirstOrDefault();
        }
        public IEnumerable<BusySeats> GetSeatsBusy(int seanceId)
        {
            var datas = db.BusySeats.Include(id => id.SeatNumber).Where(ids => ids.SeanceId.Equals(seanceId));
            return datas;
        }

    }
}
