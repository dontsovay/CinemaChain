using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface IOwnerRepository: IRepository<Owners>
    {
        IEnumerable<Owners> GetAll();
        Owners Get(int id);
        Owners Create(Owners entity);
        Owners Update(Owners entity);
        void Delete(int id);
    }
}
