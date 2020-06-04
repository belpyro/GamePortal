using AutoMapper;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Profiles
{
    class TextSetProfile: Profile
    {
        public TextSetProfile()
        {
            CreateMap<TextSetDb, TextSetDto>().ReverseMap();
        }
        
    }
}
