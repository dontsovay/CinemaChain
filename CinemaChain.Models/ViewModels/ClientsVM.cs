using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class ClientsVM
    {
        public int ClientId { get; set; }
        public string Username {get; set;}
        public string FIO { get; set; }
        public string Phone { get; set; }

        public ClientsVM(Clients clients)
        {
            Username = clients.Users.UserName;
            FIO = clients.FIO;
            Phone = clients.Phone;
        }
    }
}
