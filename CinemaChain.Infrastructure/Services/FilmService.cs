using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Services
{
    public class FilmService: IFilmService
    {
        UnitOfWork unitOfWork;

        public FilmService(DB_Context context)
        {
            unitOfWork = new UnitOfWork(context);
        }


        public IEnumerable<Films> GetFilms()
        {
            return unitOfWork.Films.GetAll();
        }

        public Films GetFilmsId(int filmId)
        {
            return unitOfWork.Films.Get(filmId);
        }
        public IEnumerable<Films> GetFilmsName(string filmName)
        {
            return unitOfWork.Films.GetAll().Where(nf => nf.FilmName.Equals(filmName));
        }

        public void DeleteFilm(int id)
        {
            unitOfWork.Films.Delete(id);
        }
        public Films UpdateFilm(Films film)
        {
            return unitOfWork.Films.Update(film);
        }
        public Films CreateFilm(Films films)
        {
            Films film = new Films();
            
            film.FilmName = films.FilmName;
            film.StartDate = films.StartDate;
            film.EndDate = films.EndDate;
            film.Description = films.Description;
            return unitOfWork.Films.Create(film);
        }
        public Films GetDatesOFFilm(int filmId)
        {
            return GetDatesOFFilm(filmId);
        }
        public bool OverdueFilm(int filmId)
        {
            var datas = GetDatesOFFilm(filmId);
            DateTime de = datas.EndDate;
            DateTime now = DateTime.Now;
            if (de < now)
            {
                return true;
            }
            else return false;

        }
    }
}
