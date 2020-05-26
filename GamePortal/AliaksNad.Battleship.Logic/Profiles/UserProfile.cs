using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper; // or Mapster
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Profiles
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDb, UserDto>()
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
