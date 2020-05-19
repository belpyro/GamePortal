using System.Collections.Generic;

namespace Nmihay.BlackJack.Model
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<GameModel> Games { get; set; }
    }
}
