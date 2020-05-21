using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Logic.Models
{
    public class ScoreDTO
    {
        int Id { get; set; }
        int PlayerID {get; set;}
        int Score { get; set; }
    }
}
