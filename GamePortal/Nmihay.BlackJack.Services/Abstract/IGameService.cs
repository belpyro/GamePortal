using Nmihay.BlackJack.Domain;

namespace Nmihay.BlackJack.Services.Abstract
{
    public interface IGameService
    {
        void AddGame(GameDto player);
        void UpdateGame(GameDto player);
        GameDto GetGameById(int id);
        void DeleteGameById(int id);
    }
}
