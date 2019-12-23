using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace CinemaChain.API.AuthModel
{
    public class AppUser : IdentityUser
    {

        public string FIO { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}

