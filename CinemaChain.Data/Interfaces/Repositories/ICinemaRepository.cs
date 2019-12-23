using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface ICinemaRepository: IRepository<Cinemas>
    {
        IEnumerable<Cinemas> GetAll();
        Cinemas Get(int id);
        Cinemas Create(Cinemas entity);
        Cinemas Update(Cinemas entity);
        void Delete(int id);
    }
}
