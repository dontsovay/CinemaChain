using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaChain.API.ViewModels.Validations;
using FluentValidation;

namespace CinemaChain.API.ViewModels
{
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
