using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaChain.API.ViewModels.Validations
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(vm => vm.Username).NotEmpty().WithMessage("username cannot be empty");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty"); 
            RuleFor(vm => vm.FIO).NotEmpty().WithMessage("FIO cannot be empty");
            RuleFor(vm => vm.Address).NotEmpty().WithMessage("Address cannot be empty");
            RuleFor(vm => vm.Phone).NotEmpty().WithMessage("Phone cannot be empty");
        }
    }
}
