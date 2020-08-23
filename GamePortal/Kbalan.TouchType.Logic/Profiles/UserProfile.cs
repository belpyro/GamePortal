using AutoMapper;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserSettingStatisticDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUser>();
            CreateMap<ApplicationUser, UserStatisticDto>().ReverseMap();
            CreateMap<ApplicationUser, UserSettingDto>().ReverseMap();

        }
    }

}
