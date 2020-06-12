using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper; // or Mapster

namespace AliaksNad.Battleship.Logic.Profiles
{
    class BattleAreaProfile : Profile
    {
        public BattleAreaProfile()
        {
            CreateMap<BattleAreaDb, BattleAreaDto>()
                .ReverseMap();
        }
    }
}
