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
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDb, UserDto>().ReverseMap();
        }
    }

    class UserStatisticProfile : Profile
    {
        public UserStatisticProfile()
        {
            CreateMap<UserStatisticDb, UserStatisticDto>().ReverseMap();
        }
    }

    class UserSettingProfile : Profile
    {
        public UserSettingProfile()
        {
            CreateMap<UserSettingDb, UserSettingDto>().ReverseMap();
        }
    }

}
