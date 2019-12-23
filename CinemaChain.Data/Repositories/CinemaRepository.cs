using CinemaChain.Data.Context;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Repositories
{
    public class CinemaRepository : BaseRepository<Cinemas>, ICinemaRepository
    {
        private DB_Context db;

        public CinemaRepository(DB_Context context) : base(context)
        {
            this.db = context;
        }


        public Cinemas GetCount(int cinemaId)
        {

            return db.Cinemas.Where(i => i.Id.Equals(cinemaId)).FirstOrDefault();
        }


        public IEnumerable<Cinemas> GetNameAdrCount(string cinemaName)
        {

            var names = db.Cinemas.Include(n => n.CinemaName)
                                  .Include(a => a.Address)
                                  .Include(cs => cs.CountSeats)
                                  .Where(n => n.CinemaName.Equals(cinemaName));

            return names;
        }


        public override IEnumerable<Cinemas> GetAll()
        {
            return db.Set<Cinemas>().ToList();
        }

        public override Cinemas Get(int id)
        {
            return db.Set<Cinemas>().Find(id);
        }




        public override Cinemas Create(Cinemas cinema)
        {
            db.Set<Cinemas>().Add(cinema);
            db.SaveChanges();
            return cinema;

        }

        public Cinemas Update(CinemaUpdVM cinemas)
        {
            db.Entry(cinemas).State = EntityState.Modified;
            db.SaveChanges();
            return db.Cinemas.Find(cinemas.Id);
        }

        public override void Delete(int id)
        {
            var cinema = db.Set<Cinemas>().Find(id);
            if (cinema == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Set<Cinemas>().Remove(cinema);
                db.SaveChanges();
            }
        }


    }
}

