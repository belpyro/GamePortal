using Nmihay.BlackJack.Domain;
using Nmihay.BlackJack.Services.Abstract;
using Nmihay.BlackJack.Mappers;
using Nmihay.BlackJack.Data;

namespace Nmihay.BlackJack.Logic.Services
{
    public class GameServis : IGameService
    {
        /// <summary>
        /// Dependency invesion
        /// Inversion of control
        /// 
        /// We have a service, the domain layer to which we transfer one 
        /// of the repositories that corresponds to this contract
        /// </summary>
        private readonly IGameRepository _gameRepository;
        public GameServis(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public void AddGame(GameDto game)
        {
            _gameRepository.AddGame(game.ToEntity());
        }

        public void DeleteGameById(int id)
        {
            throw new System.NotImplementedException();
        }

        public GameDto GetGameById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateGame(GameDto game)
        {
            throw new System.NotImplementedException();
        }
    }
}
