using AutoMapper;
using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using Igro.Quoridor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Logic.Profiles
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDb, UserDTO>().ReverseMap();
        }
    }
}
