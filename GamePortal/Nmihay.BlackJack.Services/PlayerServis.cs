using Nmihay.BlackJack.Domain;
using Nmihay.BlackJack.Services.Abstract;
using Nmihay.BlackJack.Data.Repositories.Abstract;
using Nmihay.BlackJack.Mappers;

namespace Nmihay.BlackJack.Logic.Services
{
    public class PlayerServis : IPlayerServis
    {
        /// <summary>
        /// Dependency invesion
        /// Inversion of control
        /// 
        /// We have a service, the domain layer to which we transfer one 
        /// of the repositories that corresponds to this contract
        /// </summary>
        private readonly IPlayerRepository _playerRepository;
        public PlayerServis(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public void AddPlayer(PlayerDto player)
        {
            _playerRepository.AddPlayer(player.ToEntity());
        }

        public void DeletePlayerById(int id)
        {
            throw new System.NotImplementedException();
        }

        public PlayerDto GetPlayerById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePlayer(PlayerDto player)
        {
            throw new System.NotImplementedException();
        }
    }
}
