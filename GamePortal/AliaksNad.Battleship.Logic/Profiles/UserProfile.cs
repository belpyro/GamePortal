using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.User;
using AutoMapper; // or Mapster
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Cryptography.X509Certificates;

namespace AliaksNad.Battleship.Logic.Profiles
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<NewUserDto, IdentityUser>();

            CreateMap<ExternalLoginInfo, IdentityUser>()
                .ForMember(x => x.UserName, x => x.MapFrom(c => c.DefaultUserName));

            CreateMap<UserDto, IdentityUser>()
                .ReverseMap();
        }
    }
}
