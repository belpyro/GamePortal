using Nmihay.BlackJack.Mappers;
using Nmihay.BlackJack.Model;
using Nmihay.BlackJack.Services.Abstract;

namespace API.Controllers
{
    //Facade
    public class PlayerController
    {
        private readonly IPlayerServis _playerServis;
        public PlayerController(IPlayerServis playerServis)
        {
            _playerServis = playerServis;
        }

        public void AddPlayer(PlayerModel model)
        {
            _playerServis.AddPlayer(model.ToDomain());
        }
    }
}
