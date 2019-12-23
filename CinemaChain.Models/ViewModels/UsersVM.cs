using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class UsersVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public UsersVM(Users user, Clients client, Admins admin, Owners owner)
        {
            Username = user.UserName;
            Password = user.Password;
            Role = user.Role;
            if (user.Role.Equals(Role.CLIENT))
            {
                FIO = client.FIO;
                Phone = client.Phone;
                Address = client.Address;
            }
            else if (user.Role.Equals(Role.ADMIN))
            {
                FIO = admin.FIO;
                Phone = admin.Phone;
                Address = admin.Address;
            }
            else if (user.Role.Equals(Role.OWNER))
            {
                FIO = owner.FIO;
                Phone = owner.Phone;
                Address = owner.Address;
            }
        }
    }
}
