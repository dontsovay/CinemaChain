using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Services
{
    public class ClientService: IClientService
    {
        UnitOfWork unitOfWork;

        public ClientService(DB_Context context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Clients> getClients()
        {
            return unitOfWork.Clients.GetAll();
        }
        public Admins getAdminId(string entered_username)
        {
            return unitOfWork.Admins.GetAll().Where(f => f.Users.UserName.Equals(entered_username)).FirstOrDefault();
        }
        public string getFIO(string entered_username)
        {
            var data = unitOfWork.Clients.GetAll().Where(nf => nf.Users.UserName.Equals(entered_username)).FirstOrDefault();
            return data.FIO;

        }

        public void deleteClient(int id)
        {
            unitOfWork.Clients.Delete(id);
        }

        public Clients CreateClient(Clients clients)
        {

            Clients client = new Clients();
            client.FIO = clients.FIO;
            client.Address = clients.Address;
            client.Phone = clients.Phone;
            client.UserName = clients.UserName;
            return unitOfWork.Clients.Create(client);

        }
    }
}
