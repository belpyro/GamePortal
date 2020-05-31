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
            CreateMap<UserDb, UserSettingStatisticDto>().ReverseMap();
            CreateMap<UserDb, UserDto>().ReverseMap();
            CreateMap<UserSettingDto, UserDb>()
                .ForMember(x => x.Statistic, d => d.MapFrom( m => new StatisticDb { AvarageSpeed =0, MaxSpeedRecord =0, NumberOfGamesPlayed =0} ) );
            CreateMap<UserDb, UserSettingDto>();
            CreateMap<UserDb, UserDb>();
            
        }
    }

}
