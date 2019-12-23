using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface IBusySeatService
    {
        IEnumerable<BusySeats> GetSeatsBusy();
        bool IsSeatBusy(int seanceId, int seatId);
        bool IsSeatPaid(int seatId, int seanceId);
        void CreateSeatBusy(BusySeats seats);
        IEnumerable<BusySeats> GetSeatsBusyIdSeance(int seanceId);
        BusySeats UpdateSeatBusy(int seanceId, int seatNumber, bool isbusy);
        BusySeats UpdateSeatBusyDelete(int seanceId, int seatNumber, bool isBusy);
        int GetSeat(int busyId);
        int GetBusy(int seanceId, int seatId);

    }
}
