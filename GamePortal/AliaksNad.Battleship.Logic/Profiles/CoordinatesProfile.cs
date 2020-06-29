using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper; // or Mapster

namespace AliaksNad.Battleship.Logic.Profiles
{
    public class CoordinatesProfile : Profile
    {
        public CoordinatesProfile()
        {
            CreateMap<CoordinatesDb, CoordinatesDto>()
                .ReverseMap();
                //.ForMember(x => x.IsDamaged, opt => opt.Ignore());
        }
    }
}
