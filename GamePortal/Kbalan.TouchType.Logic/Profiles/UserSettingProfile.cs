using AutoMapper;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;

namespace Kbalan.TouchType.Logic.Profiles
{
    public class UserSettingProfile : Profile
    {
        public UserSettingProfile()
        {
            CreateMap<SettingDb, SettingDto>().ReverseMap();
        }
    }

}
