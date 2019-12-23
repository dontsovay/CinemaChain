using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaChain.Infrastructure.Services
{
    public class BusySeatService: IBusySeatService
    {
        UnitOfWork unitOfWork;

        public BusySeatService(DB_Context context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<BusySeats> GetSeatsBusy()
        {
            return unitOfWork.BusySeats.GetAll();
        }

        public int GetBusy(int id_seance, int id_seat)
        {
            return unitOfWork.BusySeats.GetId(id_seance, id_seat).Id;
        }
        public int GetSeat(int id_busy)
        {
            return unitOfWork.BusySeats.GetIdBusy(id_busy).Result.SeatNumber;
        }
        public IEnumerable<BusySeats> GetSeatsBusyIdSeance(int entered_idseance)
        {
            return unitOfWork.BusySeats.GetBySeanceId(entered_idseance);
        }

        public bool IsSeatBusy(int entered_idseance, int entered_idseat)
        {
            var data = unitOfWork.BusySeats.GetAll().Where(s => s.SeatNumber.Equals(entered_idseat)).Where(ss => ss.SeanceId.Equals(entered_idseance)).FirstOrDefault();
            if (data.IsBusy == true)
            {
                return true;
            }
            else return false;
        }
        public bool IsSeatPaid(int seatId, int seanceId)
        {
            //var datas = unitOfWork.SeatsBusy.GetAll().Where(id => id.Id_Seatbusy.Equals(entered_idseat)).Where(id => id.Id_Seance.Equals(entered_idseance)).FirstOrDefault();
            var dataorder = unitOfWork.Orders.GetAll().Where(id => id.SeanceId.Equals(seanceId)).Where(id => id.SeatNumber.Equals(seatId)).FirstOrDefault();
            if (dataorder.IsPaid == true)
            {
                return true;
            }
            else return false;
        }
        public void CreateSeatBusy(BusySeats busySeats)
        {
            BusySeats seatsbusy = new BusySeats();
            seatsbusy.SeanceId = busySeats.SeanceId;
            seatsbusy.SeatNumber = busySeats.SeatNumber;
            seatsbusy.IsBusy = false;
            unitOfWork.BusySeats.Create(seatsbusy);
        }
        public BusySeats UpdateSeatBusy(int seanceId, int seatNumber, bool isBusy)
        {
            var datas = unitOfWork.BusySeats.GetId(seanceId, seatNumber);
            datas.IsBusy = true;
            unitOfWork.BusySeats.Update(datas);
            return datas;
        }
        public BusySeats UpdateSeatBusyDelete(int seanceId, int seatNumber, bool isBusy)
        {
            var datas = unitOfWork.BusySeats.GetId(seanceId, seatNumber);
            datas.IsBusy = false;
            unitOfWork.BusySeats.Update(datas);
            return datas;
        }
    }
}
