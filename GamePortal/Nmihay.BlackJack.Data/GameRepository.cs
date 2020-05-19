using Nmihay.BlackJack.Data.Repositories.Abstract;
using Nmihay.BlackJack.Entities;
using System.Collections.Generic;

namespace Nmihay.BlackJack.Data
{
    /// <summary>
    /// CRUD Repository (Create, Read, Update, Delete)
    /// Can connect to the cloud, files, databases, InMemory
    /// </summary>
    class GameRepository : IGameRepository
    {
        private static List<GameEntity> _games = new List<GameEntity>();
        public void AddGame(GameEntity game)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteGameById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<GameEntity> GetAllGames()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateGame(GameEntity game)
        {
            throw new System.NotImplementedException();
        }
    }
}
