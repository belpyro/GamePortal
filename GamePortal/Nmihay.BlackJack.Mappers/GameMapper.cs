using Nmihay.BlackJack.Domain;
using Nmihay.BlackJack.Entities;
using Nmihay.BlackJack.Model;
using System.Linq;

namespace Nmihay.BlackJack.Mappers
{
    public static class GamerMapper
    {
        /// <summary>
        /// We accept a  Game and return the new GamerEntity into 
        /// which we transfer everything that we have
        /// </summary>
        /// <param name="game"></param>
        /// <returns>return the new GameEntity into 
        /// which we transfer everything that we have</returns>
        public static GameEntity ToEntity(this GameDto game)  //ToModel ToDomain ...
        {
            return new GameEntity
            {
                Title = game.Title,
                PlayerId = game.PlayerId
            };
        }

        public static GameDto ToDomain(this GameModel game)
        {
            if (game == null) return null;

            return new GameDto
            {
                Title = game.Title,
                PlayerId = game.PlayerId,

                Players = game.Players.Select(x => x.ToDomain()).ToList()
            };
        }
    }
}
