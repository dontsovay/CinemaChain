using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface ISeatRepository: IRepository<Seats>
    {
        IEnumerable<Seats> GetAll();
        Seats Get(int id);
        Seats Create(Seats entity);
        Seats Update(Seats entity);
        void Delete(int id);
    }
}
