using AutoMapper;
using Vitaly.Sapper.Data.Contexts;
using Vitaly.Sapper.Data.Models;
using Vitaly.Sapper.Logic.Models;

namespace Vitaly.Sapper.Logic.Profiles
{
    class VitalySapperUserProfile : Profile
    {
        public VitalySapperUserProfile()
        {
            CreateMap<UserDb, UserDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.Pwd, opt => opt.MapFrom(x => x.Pwd))
                .ReverseMap();
        }
    }
}
