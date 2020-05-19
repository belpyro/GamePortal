using System.Collections.Generic;

namespace Nmihay.BlackJack.Model
{
    public class GameModel
    {
        public string Title { get; set; }
        public int PlayerId { get; set; }
        public List<PlayerModel> Players { get; set; }
    }
}
