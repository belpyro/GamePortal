using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using AutoMapper; // or Mapster
using Castle.Core.Internal;

namespace AliaksNad.Battleship.Logic.Profiles
{
    class BattleAreaProfile : Profile
    {
        public BattleAreaProfile()
        {
            CreateMap<BattleAreaDb, NewBattleAreaDto>()
                .ReverseMap();
        }
    }
}
