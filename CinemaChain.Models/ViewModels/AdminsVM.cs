using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class AdminsVM
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }

        public AdminsVM(Admins admins)
        {
            Username = admins.Users.UserName;
            FIO = admins.FIO;
            Phone = admins.Phone;
        }
    }
}
