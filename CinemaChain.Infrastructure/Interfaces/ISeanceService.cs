using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface ISeanceService
    {
        IEnumerable<Seances> GetSeances();
        Seances GetSeanceId(int seanceId);
        Seances GetDateofSeance(int seanceId);
        void DeleteSeance(int id);
        int CountSeats(int seanceId);
        int SeatDecrement(int seanceId);

        void CreateSeance(SeancesVM seances);

       // bool OverdueSeance(int seanceId);
        Seances UpdateSeance(Seances seance);
        Seances GetInfoAboutSeanceandFilm(int busyId);
    }
}
