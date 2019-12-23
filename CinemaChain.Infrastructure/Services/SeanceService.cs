using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Services
{
    public class SeanceService : ISeanceService
    {
        UnitOfWork unitOfWork;
        private readonly IBusySeatService _busyseatService;
        private readonly IFilmService _filmService;
        public SeanceService(DB_Context context, IFilmService filmService, IBusySeatService busyseatService)
        {
            unitOfWork = new UnitOfWork(context);
            _filmService = filmService;
            _busyseatService = busyseatService;
        }
        public IEnumerable<Seances> GetSeances()
        {
            return unitOfWork.Seances.GetAll();
        }

        public Seances GetInfoAboutSeanceandFilm(int busyId)
        {
            var seance = unitOfWork.BusySeats.GetIdBusy(busyId).Result.SeanceId;
            var data = unitOfWork.Seances.Get(seance);
            return data;
        }

        public Seances GetSeanceId(int seanceId)
        {
            return unitOfWork.Seances.Get(seanceId);
        }
        public Seances GetBusyId(int seanceId)
        {
            return unitOfWork.Seances.Get(seanceId);
            //return unitOfWork.Seances.GetAll().Where(f => f.Id_Seance.Equals(entered_idseance));
        }

        public Seances GetDateofSeance(int seanceId)
        {
            return GetDateofSeance(seanceId); ///????????????
        }
        public Seances UpdateSeance(Seances seance)
        {
            if (seance == null)
            {
                throw new ArgumentNullException();
            }
            var datas = unitOfWork.Seances.GetAll().Where(i => i.Id.Equals(seance.Id)).FirstOrDefault();
            datas.Price = seance.Price;
            return unitOfWork.Seances.Update(datas);
        }

        public void DeleteSeance(int id)
        {
            unitOfWork.Seances.Delete(id);
            //var idseance = unitOfWork.Orders.GetSeance(id);
            //if (unitOfWork.Orders.GetSeance(id) != null)
            //{
            //    foreach (var idseance in unitOfWork.Orders.GetSeance(id))
            //    {
            //        unitOfWork.Orders.Delete(idseance.id_seance);
            //    }

            //    foreach (var idseat in unitOfWork.SeatsBusy.GetSeatsBusy(id))
            //    {
            //        unitOfWork.SeatsBusy.Delete(idseat);
            //    }
            //}
        }
        //public bool OverdueSeance(int seanceId)
        //{
        //    DateTime now = DateTime.Now;
        //    var datas = GetDateofSeance(seanceId);
        //    DateTime date = datas.SeanceDate;
        //    TimeSpan time = datas.SeanceTime;
        //    if (date >= now)
        //    {
        //        if (time.Hours >= now.Hour)
        //        {
        //            if (time.Minutes > now.Minute)
        //            {где модель и контроллер?

        //                return false;
        //            }
        //            else return true;
        //        }
        //        else return true;
        //    }
        //    else return true;

        // }

        public int CountSeats(int seanceId)
        {
            var datas = unitOfWork.Seances.GetAll().Where(id => id.Id.Equals(seanceId)).FirstOrDefault();
            return datas.CountSeats;

        }

        public int SeatDecrement(int seanceId)
        {
            var datas = unitOfWork.Seances.Get(seanceId);
            datas.CountSeats = datas.CountSeats - 1;
            unitOfWork.Seances.Update(datas);
            return datas.CountSeats; 
        }
        public void CreateSeance(SeancesVM seances)
        {
            var cinemaRes = unitOfWork.Cinemas.GetCount(seances.cinemaId);
            if (cinemaRes.CountSeats.CompareTo(null) != 0)
            {

                //TimeSpan hour = TimeSpan.FromTicks();
                Seances seance = new Seances();
                var datas = unitOfWork.Films.Get(seances.cinemaId);
                {
                    seance.SeanceName = seances.seanceName;
                    seance.CinemaId = seances.cinemaId;
                    seance.FilmId = seances.filmId;
                    seance.SeanceDate = seances.seanceDate;

                    seance.Price = seances.price;
                    seance.CountSeats = cinemaRes.CountSeats;
                    seance.AllSeats = cinemaRes.CountSeats;
                    unitOfWork.Seances.Create(seance);
                    
                    var thisseance = unitOfWork.Seances.GetId(seances.cinemaId, seances.filmId, seances.seanceDate, seances.price).Id;
                    if (thisseance.CompareTo(null) != 0)
                    {
                        var COUNTSEATS = unitOfWork.Seances.Get(thisseance).AllSeats;
                        BusySeats busy = new BusySeats();
                        busy.SeanceId = thisseance;
                        busy.SeatNumber = COUNTSEATS;

                        _busyseatService.CreateSeatBusy(busy);
                    }
                }
            }
        }
    }
}