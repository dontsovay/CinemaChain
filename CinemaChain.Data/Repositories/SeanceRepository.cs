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
    public class SeanceRepository : BaseRepository<Seances>, ISeanceRepository
    {
        private DB_Context db;

        public SeanceRepository(DB_Context context) : base(context)
        {
            this.db = context;
        }
        public override IEnumerable<Seances> GetAll()
        {
            return db.Set<Seances>().ToList();
        }
        public Seances getDateofSeance(int seanceId)
        {
            return db.Seances.Include(d => d.SeanceDate).Where(id => id.Id.Equals(seanceId)).FirstOrDefault();
            //Include(d => d.Date_Seance).Include(t=>t.Time_Seance);
        }

        public override Seances Get(int id)
        {
            Seances seances = db.Seances.Where(i => i.Id.Equals(id)).FirstOrDefault();
            return seances;
        }

        public Seances GetId(int cinemaId, int filmId, DateTime seanceDate, int price)
        {
            return db.Seances.FirstOrDefault(id => id.CinemaId.Equals(cinemaId) && id.FilmId.Equals(filmId) && id.SeanceDate.Equals(seanceDate) && id.Price.Equals(price));
        }
        public override Seances Create(Seances seance)
        {
            db.Set<Seances>().Add(seance);
            db.SaveChanges();
            return seance;
        }

        public override Seances Update(Seances seance)
        {
            if (seance.Price.CompareTo(null)!=0)
            {
                db.Entry(seance).Property(c => c.Price).IsModified = true;
            }
            else if (seance.CountSeats.CompareTo(null)!=0)
            { 
                db.Entry(seance).Property(cc => cc.CountSeats).IsModified = true;
            }
            db.SaveChanges();
            return seance;

        }

        public override void Delete(int id)
        {

            Seances seances = db.Seances.Where(i => i.Id.Equals(id)).FirstOrDefault();
            if (seances == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Seances.Remove(seances);
                db.SaveChanges();
            }

        }
    }
}
