using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper; // or Mapster
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Cryptography.X509Certificates;

namespace AliaksNad.Battleship.Logic.Profiles
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserDto>()
                .ReverseMap();
        }
    }
}
