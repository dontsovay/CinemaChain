using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace CinemaChain.Models.ViewModels.Mappers
{
    class ViewModelToEntityMappingProfile: Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationVM, API.AuthModel.AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Username));
        }
    }
}
