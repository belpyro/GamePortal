using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Services.Contracts
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
        /// Check enemy ship with target coordinates.
        /// </summary>
        /// <param name="target">Enemy coordinates.</param>
        /// <returns></returns>
        Task<Result<Maybe<TargetDto>>> CheckTargetAsync(TargetDto target);

        /// <summary>
        /// Delete battle area by id.
        /// </summary>
        /// <param name="id">BattleArea id.</param>
        /// <returns></returns>
        Task<Result> DeleteBattleAreaAsync(int id);
    }
}
