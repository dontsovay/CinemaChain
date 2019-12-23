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
    public class FilmImageRepository: BaseRepository<FilmImages>, IFilmImageRepository
    {
        private DB_Context db;

        public FilmImageRepository(DB_Context context) : base(context)
        {
            this.db = context;
        }
        public override IEnumerable<FilmImages> GetAll()
        {
            return db.Set<FilmImages>().ToList();
        }

        public override FilmImages Get(int id)
        {
            return db.Set<FilmImages>().Find(id);
        }

        public override FilmImages Create(FilmImages image)
        {
            db.Set<FilmImages>().Add(image);
            db.SaveChangesAsync();
            return image;

        }

        public override FilmImages Update(FilmImages image)
        {
            db.Entry(image).State = EntityState.Modified;
            db.SaveChangesAsync();
            return image;
        }

        public override void Delete(int id)
        {
            var filmimage = db.Set<FilmImages>().Find(id);
            if (filmimage == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Set<FilmImages>().Remove(filmimage);
                db.SaveChangesAsync();
            }
        }
        public byte[] GetFilmImage(int filmId)
        {
            byte[] image =  db.FilmImages.Where(i => i.FilmId.Equals(filmId)).FirstOrDefault().FilmImage;
            return image;
            
        }
        //public IEnumerable<byte[]> GetFilmImages(int filmId)
        //{
        //    IEnumerable<byte[]> image = db.FilmImages.Include(k => k.FilmImage).AsEnumerable().Where(i => i.FilmId.Equals(filmId)));
        //    return image;
        //}
    }
}
