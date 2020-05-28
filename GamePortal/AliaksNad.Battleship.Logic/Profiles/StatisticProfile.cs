using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper; // or Mapster

namespace AliaksNad.Battleship.Logic.Profiles
{
    class StatisticProfile : Profile
    {
        public StatisticProfile()
        {
            CreateMap<StatisticDb, StatisticDto>()
                .ReverseMap();
        }
    }
}
