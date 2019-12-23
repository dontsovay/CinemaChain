using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface IClientService
    {
        IEnumerable<Clients> getClients();
        Admins getAdminId(string entered_username);
        string getFIO(string entered_username);
        void deleteClient(int id);
        Clients CreateClient(Clients clients);
    }
}
