using System.Collections.Generic;

namespace Nmihay.BlackJack.Domain
{
    public class GameDto
    {
        public string Title { get; set; }
        public int PlayerId { get; set; }
        public List<PlayerDto> Players { get; set; }
    }
}
