using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        IEnumerable<Admins> GetAll();
        Admins Get(int id);
        Admins Create(Admins entity);
        Admins Update(Admins entity);
        void Delete(int id);

    }
}
