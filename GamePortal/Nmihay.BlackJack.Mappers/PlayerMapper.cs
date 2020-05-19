using Nmihay.BlackJack.Domain;
using Nmihay.BlackJack.Entities;
using Nmihay.BlackJack.Model;
using System.Linq;

namespace Nmihay.BlackJack.Mappers
{
    public static class PlayerMapper
    {
        /// <summary>
        /// We accept a  Player and return the new PlayerEntity into 
        /// which we transfer everything that we have
        /// </summary>
        /// <param name="player"></param>
        /// <returns>return the new PlayerEntity into 
        /// which we transfer everything that we have</returns>
        public static PlayerEntity ToEntity(this PlayerDto player)  //ToModel ToDomain ...
        {
            return new PlayerEntity
            {
                Name = player.Name,
                Age = player.Age
            };
        }

        public static PlayerDto ToDomain(this PlayerModel player)
        {
            if (player == null) return null;

            return new PlayerDto
            {
                Name = player.Name,
                Age = player.Age,

                Games = player.Games.Select(x => x.ToDomain()).ToList()
            };
        }
    }
}
