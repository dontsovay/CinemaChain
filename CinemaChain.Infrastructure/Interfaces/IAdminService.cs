using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<Admins> getAdmins();
        Admins getAdminId(string entered_username);
        string getFIO(string entered_username);
        void deleteAdmin(int id);
        Admins CreateAdmin(Admins admins);
    }
}
