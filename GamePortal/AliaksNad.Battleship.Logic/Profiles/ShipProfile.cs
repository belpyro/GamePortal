using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using AutoMapper;

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
