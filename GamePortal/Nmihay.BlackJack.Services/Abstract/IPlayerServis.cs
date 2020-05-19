using Nmihay.BlackJack.Domain;

namespace Nmihay.BlackJack.Services.Abstract
{
    public interface IPlayerServis
    {
        void AddPlayer(PlayerDto player);
        void UpdatePlayer(PlayerDto player);
        PlayerDto GetPlayerById(int id);
        void DeletePlayerById(int id);
    }
}
