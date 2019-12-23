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
    public class FilmRepository : BaseRepository<Films>, IFilmRepository
    {
        private DB_Context db;

        public FilmRepository(DB_Context context) : base(context)
        {
            this.db = context;
        }

        public override IEnumerable<Films> GetAll()
        {
            return db.Set<Films>().ToList();
        }

        public override Films Get(int id)
        {
            return db.Set<Films>().Find(id);
        }

        public override Films Create(Films film)
        {
            db.Set<Films>().Add(film);
            db.SaveChanges();
            return film;

        }

        public override Films Update(Films film)
        {
            db.Entry(film).State = EntityState.Modified;
            db.SaveChanges();
            return film;
        }

        public override void Delete(int id)
        {
            var film = db.Set<Films>().Find(id);
            if (film == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Set<Films>().Remove(film);
                db.SaveChangesAsync();
            }
        }
        public Films GetFilmName(int filmId)
        {
            return db.Films.Where(i => i.Id.Equals(filmId)).FirstOrDefault();
        }

    }
}
