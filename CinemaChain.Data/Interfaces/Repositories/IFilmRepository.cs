using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface IFilmRepository: IRepository<Films>
    {
        IEnumerable<Films> GetAll();
        Films Get(int id);
        Films Create(Films entity);
        Films Update(Films entity);
        void Delete(int id);
    }
}
