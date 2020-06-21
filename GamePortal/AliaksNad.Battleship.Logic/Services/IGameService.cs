using AliaksNad.Battleship.Logic.Models;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Services
{
    public interface IGameService
    {
        /// <summary>
        /// Set your own fleet coordinates.
        /// </summary>
        /// <param name="fleetCoordinates">Own fleet coordinates.</param>
        Task<Result<BattleAreaDto>> AddAsync(BattleAreaDto fleetCoordinates);

        /// <summary>
        /// Get all battle area. 
        /// </summary>
        Task<Result<IEnumerable<BattleAreaDto>>> GetAllAsync();

        /// <summary>
        /// Get battle area by id.
        /// </summary>
        /// <param name="id">user id.</param>
        Task<Result<Maybe<BattleAreaDto>>> GetByIdAsync(int id);

        /// <summary>
        /// Checking hit by enemy coordinates.
        /// </summary>
        /// <param name="coordinates">Enemy coordinates.</param>
        /// <returns></returns>
        Task<Result<Maybe<CoordinatesDto>>> CheckHitAsync(CoordinatesDto coordinates);
    }
}
