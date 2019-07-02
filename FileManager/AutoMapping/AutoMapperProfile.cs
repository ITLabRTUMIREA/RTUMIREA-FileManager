using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileManager.Models;
using FileManager.Models.ViewModels.Account;

namespace FileManager.AutoMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SignUpViewModel, User>()
                 .ForMember("UserName", opt => opt.MapFrom(src => src.Email));
    
        }
    }
}
