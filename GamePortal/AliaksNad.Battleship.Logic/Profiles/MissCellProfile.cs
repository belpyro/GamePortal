using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using AutoMapper;

namespace AliaksNad.Battleship.Logic.Profiles
{
    class MissCellProfile : Profile
    {
        public MissCellProfile()
        {
            CreateMap<MissCellDb, NewMissCellDto>()
                .ReverseMap();
        }
    }
}
