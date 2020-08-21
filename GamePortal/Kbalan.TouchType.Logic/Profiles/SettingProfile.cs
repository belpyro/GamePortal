using AutoMapper;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;

namespace Kbalan.TouchType.Logic.Profiles
{
    public class SettingProfile : Profile
    {
        public SettingProfile()
        {
            CreateMap<SettingDb, SettingDto>().ForMember("Avatar", opt => opt.MapFrom(src => src.AvatarUrl)).ReverseMap();
        }
    }

}
