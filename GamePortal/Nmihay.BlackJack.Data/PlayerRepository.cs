using Nmihay.BlackJack.Data.Repositories.Abstract;
using Nmihay.BlackJack.Entities;
using System.Collections.Generic;

namespace Nmihay.BlackJack.Data
{
    /// <summary>
    /// CRUD Repository (Create, Read, Update, Delete)
    /// Can connect to the cloud, files, databases, InMemory
    /// </summary>
    class PlayerRepository : IPlayerRepository
    {
        private static List<PlayerEntity> _players = new List<PlayerEntity>();
        public void AddPlayer(PlayerEntity player)
        {
            throw new System.NotImplementedException();
        }

        public void DeletePlayerById(int id)
        {
            throw new System.NotImplementedException();
        }

        public PlayerEntity GetPlayerById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateDriver(PlayerEntity player)
        {
            throw new System.NotImplementedException();
        }
    }
}
