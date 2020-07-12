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
            CreateMap<CoordinatesDb, NewCoordinatesDto>()
                .ReverseMap();
        }
    }
}
