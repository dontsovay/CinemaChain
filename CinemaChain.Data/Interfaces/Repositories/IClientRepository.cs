using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Clients> GetAll();
        Clients Get(int id);
        Clients Create(Clients entity);
        Clients Update(Clients entity);
        void Delete(int id);
    }
}
