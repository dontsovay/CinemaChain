using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface ISeanceRepository: IRepository<Seances>
    {
        IEnumerable<Seances> GetAll();
        Seances Get(int id);
        Seances Create(Seances entity);
        Seances Update(Seances entity);
        void Delete(int id);
    }
}
