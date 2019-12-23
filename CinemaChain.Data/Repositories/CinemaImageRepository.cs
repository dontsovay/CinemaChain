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
    public class CinemaImageRepository : BaseRepository<CinemaImages>, ICinemaImageRepository
    {
        private DB_Context db;

        public CinemaImageRepository(DB_Context context) : base(context)
        {
            this.db = context;
        }
        public override IEnumerable<CinemaImages> GetAll()
        {
            return db.Set<CinemaImages>().ToList();
        }

        public override CinemaImages Get(int id)
        {
            return db.Set<CinemaImages>().Find(id);
        }

        public override CinemaImages Create(CinemaImages image)
        {
            db.Set<CinemaImages>().Add(image);
            db.SaveChangesAsync();
            return image;

        }

        public override CinemaImages Update(CinemaImages image)
        {
            db.Entry(image).State = EntityState.Modified;
            db.SaveChangesAsync();
            return image;
        }

        public override void Delete(int id)
        {
            var cinemaimage = db.Set<CinemaImages>().Find(id);
            if (cinemaimage == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Set<CinemaImages>().Remove(cinemaimage);
                db.SaveChangesAsync();
            }
        }
        public byte[] GetCinemaImage(int cinemaId)
        {
            byte[] image = db.CinemaImages.Where(i => i.CinemaId.Equals(cinemaId)).FirstOrDefault().CinemaImage;
            return image;

        }
        //public IEnumerable<byte[]> GetFilmImages(int filmId)
        //{
        //    IEnumerable<byte[]> image = db.FilmImages.Include(k => k.FilmImage).AsEnumerable().Where(i => i.FilmId.Equals(filmId)));
        //    return image;
        //}
    }
}
