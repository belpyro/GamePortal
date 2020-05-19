using Nmihay.BlackJack.Mappers;
using Nmihay.BlackJack.Model;
using Nmihay.BlackJack.Logic.Services;

namespace API.Controllers
{
    public class GameController
    {
        private readonly GameServis _playerServis;
        public GameController(GameServis playerServis)
        {
            _playerServis = playerServis;
        }

        public void AddGame(GameModel model)
        {
            _playerServis.AddGame(model.ToDomain());
        }
    }
}
