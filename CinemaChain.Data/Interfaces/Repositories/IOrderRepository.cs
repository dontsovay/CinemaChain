using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface IOrderRepository: IRepository<Orders>
    {
        IEnumerable<Orders> GetAll();
        Orders Get(int id);
        Orders Create(Orders entity);
        Orders Update(Orders entity);
        void Delete(int id);
    }
}
