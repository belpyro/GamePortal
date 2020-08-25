using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    /// Dto for transfering game status after turn
    /// 0 - incorrect turn
    /// 1 - correct turn and game isn't finished
    /// 2 - correct turn, game finished
    /// </summary>
    public class SingleGameResultDto
    {
        [Range(0, 2,
        ErrorMessage = "Value for must be from 0 to 2.")]
        public int TurnResult;
    }
}
