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
    public class CinemaService : ICinemaService
    {
        UnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly ISeatService seatService;

        public CinemaService(DB_Context context, IUserService usService, ISeatService stService)
        {
            unitOfWork = new UnitOfWork(context);
            userService = usService;
            seatService = stService;
            // userService.id_owner = 1;
        }

        public IEnumerable<Cinemas> GetCinemas()
        {
            return unitOfWork.Cinemas.GetAll();
        }

        public Cinemas GetCinemasId(int cinemaId)
        {
            return unitOfWork.Cinemas.Get(cinemaId);
        }
        public IEnumerable<Cinemas> GetCinemaAdress(string entered_namecinema)
        {
            return unitOfWork.Cinemas.GetNameAdrCount(entered_namecinema);
        }

        public void DeleteCinema(int id)
        {
            unitOfWork.Cinemas.Delete(id);
        }

        public void CreateCinema(Cinemas cinemas)
        {

            Cinemas cinema = new Cinemas();
            cinema.CinemaName = cinemas.CinemaName;
            cinema.Address = cinemas.Address;
            cinema.CountSeats = cinemas.CountSeats;
            unitOfWork.Cinemas.Create(cinema);//открыла соединение 
            var id = unitOfWork.Cinemas.GetAll()
                .Where(i => i.CinemaName.Equals(cinema.CinemaName))
                .Where(i => i.Address.Equals(cinema.Address))
                .Where(i => i.CountSeats.Equals(cinema.CountSeats))
                .FirstOrDefault().Id; 

            int countseats = cinema.CountSeats;
            if (countseats != 0)
            {
                    seatService.CreateSeat(id, countseats);// открыла еще
            }

        }
        public Cinemas GetCinemaNameId(int cinemaId)
        {
            return unitOfWork.Cinemas.GetAll().Where(i => i.Id.Equals(cinemaId)).FirstOrDefault();
        }
        public bool IsCinemaExists(int cinemaId)
        {
            var data = unitOfWork.Cinemas.GetAll().Where(c => c.Id.Equals(cinemaId)).FirstOrDefault();
            if (data.Id != 0)
            {
                return true;
            }
            else return false;
        }

        public Cinemas UpdateCinema(CinemaUpdVM cinema)
        {
            return unitOfWork.Cinemas.Update(cinema);
        }

    }
}
