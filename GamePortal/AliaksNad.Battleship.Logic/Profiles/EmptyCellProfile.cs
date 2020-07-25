using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using AutoMapper;

namespace AliaksNad.Battleship.Logic.Profiles
{
    class EmptyCellProfile : Profile
    {
        public EmptyCellProfile()
        {
            CreateMap<EmptyCellDb, EmptyCellDto>()
                .ReverseMap();
        }
    }
}
