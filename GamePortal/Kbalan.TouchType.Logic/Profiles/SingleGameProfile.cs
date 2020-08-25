using AutoMapper;
using Kbalan.TouchType.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Profiles
{
    class SingleGameProfile : Profile
    {
        public SingleGameProfile()
        {
            CreateMap<SingleGame, SingleGame>().ReverseMap();
        }

    }
}
