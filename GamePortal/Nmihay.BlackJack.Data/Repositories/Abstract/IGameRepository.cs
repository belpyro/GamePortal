using Nmihay.BlackJack.Entities;
using System.Collections.Generic;

namespace Nmihay.BlackJack.Data
{
    public interface IGameRepository
    {
        void AddGame(GameEntity game);
        void UpdateGame(GameEntity game);
        List<GameEntity> GetAllGames();
        void DeleteGameById(int id);
    }
}
