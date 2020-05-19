using Nmihay.BlackJack.Domain;
using Nmihay.BlackJack.Services.Abstract;
using System.Collections.Generic;

namespace Nmihay.BlackJack.Services
{
    /// <summary>
    /// This decorator will log all events in the service
    /// </summary>
    public class GameServiceLoggerDecorator : IGameService
    {
        private readonly IGameService _gameService;

        /// <summary>
        /// caching
        /// add those games that were accessed during the service
        /// </summary>
        private List<GameDto> _games = new List<GameDto>();

        public GameServiceLoggerDecorator(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// In these methods additional logic will be 
        /// implemented and the service itself will be called
        /// </summary>
        /// <param name="player"></param>
        public void AddGame(GameDto game)
        {
            //logging
            _gameService.AddGame(game);
        }

        public void DeleteGameById(int id)
        {
            throw new System.NotImplementedException();
        }

        public GameDto GetGameById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateGame(GameDto player)
        {
            throw new System.NotImplementedException();
        }
    }
}
