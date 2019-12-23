using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface IFilmService
    {

        IEnumerable<Films> GetFilms();
        Films GetFilmsId(int filmId);
        IEnumerable<Films> GetFilmsName(string filmName);
        void DeleteFilm(int id);
        Films CreateFilm(Films films);
        Films GetDatesOFFilm(int filmId);
        bool OverdueFilm(int filmId);
        Films UpdateFilm(Films film);
    }
}
