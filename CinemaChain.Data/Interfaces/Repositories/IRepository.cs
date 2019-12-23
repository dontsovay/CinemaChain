using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T entity);
        T Update(T entity);
        void Delete(int id);
    }
}
