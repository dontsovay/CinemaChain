using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAll();
        Users Get(string username);
        Users Create(Users entity);
        Users Update(Users entity);
        void Delete(string username);
    }
}
