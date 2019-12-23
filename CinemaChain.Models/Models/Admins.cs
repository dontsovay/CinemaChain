using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaChain.Models.Models
{
    public class Admins 
    {
        public string Id { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }

        [ForeignKey("UserName")]
        public virtual Users Users { get; set; }
    }
}
