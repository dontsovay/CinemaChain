using CinemaChain.API.AuthModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaChain.Models.Models
{
    public enum Role
    {
        CLIENT, ADMIN, OWNER
    }
    public class Users : IdentityUser
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }
        public Role Role { get; set; }
        public string Password { get; set; }

        public ICollection<Orders> Orders { get; set; }
        public Clients Clients { get; set; }
        public Admins Admins { get; set; }
        public Owners Owners { get; set; }
        public AppUser AppUser { get; set; }
    }
}
