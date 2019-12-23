using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class OwnersVM
    {
        public int OwnerId { get; set; }
        public string Username { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }

        public OwnersVM(Owners owners)
        {
            Username = owners.UserName;
            FIO = owners.FIO;
            Phone = owners.Phone;
        }
    }
}
