using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Profiles
{
    class ShipProfile : Profile
    {
        public ShipProfile()
        {
            CreateMap<ShipDb, ShipDto>()
                .ReverseMap();
        }
    }
}
