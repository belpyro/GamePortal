using System.Collections.Generic;

namespace Nmihay.BlackJack.Domain
{
    public class PlayerDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<GameDto> Games { get; set; }
    }
}
