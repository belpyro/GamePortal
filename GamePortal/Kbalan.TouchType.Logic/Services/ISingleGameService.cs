using CSharpFunctionalExtensions;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    public interface ISingleGameService
    {
         Task<Result<NewSingleGameDto>> AddNewSingleGameAsync(string id);

         Task<Result<SingleGameResultDto>> UserTurnAsync(int id, string turn);
    }
}

