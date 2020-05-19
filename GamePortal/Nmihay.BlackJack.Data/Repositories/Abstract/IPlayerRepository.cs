using Nmihay.BlackJack.Entities;

namespace Nmihay.BlackJack.Data.Repositories.Abstract
{
    public interface IPlayerRepository
    {
        void AddPlayer(PlayerEntity player);
        void UpdateDriver(PlayerEntity player);
        PlayerEntity GetPlayerById(int id);
        void DeletePlayerById(int id);
    }
}
