using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface IBusySeatRepository: IRepository<BusySeats>
    {
        IEnumerable<BusySeats> GetAll();
        BusySeats Get(int id);
        BusySeats Create(BusySeats entity);
        BusySeats Update(BusySeats entity);
        void Delete(int id);
    }
}
