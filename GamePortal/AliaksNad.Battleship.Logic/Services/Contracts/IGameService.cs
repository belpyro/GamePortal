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
        Task<Result<NewBattleAreaDto>> AddAsync(NewBattleAreaDto fleetCoordinates);

        /// <summary>
        /// Get all battle area. 
        /// </summary>
        Task<Result<IEnumerable<NewBattleAreaDto>>> GetAllAsync();

        /// <summary>
        /// Get battle area by id.
        /// </summary>
        /// <param name="id">user id.</param>
        Task<Result<Maybe<NewBattleAreaDto>>> GetByIdAsync(int id);

        /// <summary>
        /// Checking hit by enemy coordinates.
        /// </summary>
        /// <param name="coordinates">Enemy coordinates.</param>
        /// <returns></returns>
        //Task<Result<Maybe<NewCoordinatesDto>>> CheckHitAsync(NewCoordinatesDto coordinates);
    }
}
