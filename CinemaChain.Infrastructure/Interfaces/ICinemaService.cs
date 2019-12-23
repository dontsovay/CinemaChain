using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface ICinemaService
    {
        IEnumerable<Cinemas> GetCinemas();
        Cinemas GetCinemasId(int cinemaId);
        IEnumerable<Cinemas> GetCinemaAdress(string cinemaName);
        void DeleteCinema(int id);
        void CreateCinema(Cinemas cinemas);
        bool IsCinemaExists(int cinemaId);
        Cinemas UpdateCinema(CinemaUpdVM cinema);
        Cinemas GetCinemaNameId(int cinemaId);
       
    }
}
