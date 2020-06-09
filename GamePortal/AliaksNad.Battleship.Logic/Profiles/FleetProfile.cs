using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper; // or Mapster

namespace AliaksNad.Battleship.Logic.Profiles
{
    class FleetProfile : Profile
    {
        public FleetProfile()
        {
            CreateMap<FleetDb, FleetDto>()
                .ReverseMap();
        }
    }
}
