using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper; // or Mapster
using Castle.Core.Internal;

namespace AliaksNad.Battleship.Logic.Profiles
{
    class BattleAreaProfile : Profile
    {
        public BattleAreaProfile()
        {
            CreateMap<BattleAreaDb, BattleAreaDto>()
                .ForMember(x => x.FailedLaunches.IsNullOrEmpty(), opt => opt.Ignore()) //TODO need for ignore CoordinatesDto with empty ShipsId
                .ReverseMap();
        }
    }
}
