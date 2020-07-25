using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using AutoMapper; // or Mapster

namespace AliaksNad.Battleship.Logic.Profiles
{
    public class CoordinatesProfile : Profile
    {
        public CoordinatesProfile()
        {
            CreateMap<CoordinatesDb, CoordinatesDto>()
                .ReverseMap();
        }
    }

    public class TargetProfile : Profile
    {
        public TargetProfile()
        {
            CreateMap<CoordinatesDb, TargetDto>()
                .ForMember(c => c.EnemyBattleAreaId, x => x.Ignore())
                .ReverseMap();
        }
    }
}
