using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Services.Contracts;
using AutoMapper;
using CSharpFunctionalExtensions;
using JetBrains.Annotations;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Services
{
    public class GameService : IGameService 
    {
        private readonly BattleAreaContext _battleAreaContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GameService([NotNull]BattleAreaContext _battleAreaContext,
            [NotNull]IMapper mapper,
            [NotNull]ILogger logger)
        {
            this._battleAreaContext = _battleAreaContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="BattleAreaModel">Own fleet coordinates.</param>
        public async Task<Result<NewBattleAreaDto>> AddAsync(NewBattleAreaDto BattleAreaModel)
        {
            try
            {
                var dbBattleAreaModel = _mapper.Map<BattleAreaDb>(BattleAreaModel);

                _battleAreaContext.BattleAreas.Add(dbBattleAreaModel);
                await _battleAreaContext.SaveChangesAsync().ConfigureAwait(false);

                BattleAreaModel.BattleAreaId = dbBattleAreaModel.BattleAreaId;
                return Result.Success(BattleAreaModel);
            }
            catch (DbUpdateException ex)
            {
                _logger.Warning(ex, "An error occurred while writing the model to the DB");
                return Result.Failure<NewBattleAreaDto>(ex.Message);
            }
        }

        /// <summary>
        /// Get all battle area from data.
        /// </summary>
        /// <returns></returns>
        public async Task<Result<IEnumerable<NewBattleAreaDto>>> GetAllAsync()
        {
            try
            {
                var models = await _battleAreaContext.BattleAreas.AsNoTracking()
                    .ToArrayAsync().ConfigureAwait(false);
                return Result.Success(_mapper.Map<IEnumerable<NewBattleAreaDto>>(models));
            }
            catch (SqlException ex)
            {
                _logger.Warning(ex, "An error occurred while reading models from the DB");
                return Result.Failure<IEnumerable<NewBattleAreaDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Get battle area from data by id.
        /// </summary>
        /// <param name="id">battle area id.</param>
        /// <returns></returns>
        public async Task<Result<Maybe<NewBattleAreaDto>>> GetByIdAsync(int id)
        {
            try
            {
                Maybe<NewBattleAreaDto> battleArea = await _battleAreaContext.BattleAreas.Where(x => x.BattleAreaId == id)
                    .ProjectToSingleOrDefaultAsync<NewBattleAreaDto>(_mapper.ConfigurationProvider).ConfigureAwait(false);
                return Result.Success(battleArea);
            }
            catch (SqlException ex)
            {
                _logger.Warning(ex, "An error occurred while reading model by id from the DB");
                return Result.Failure<Maybe<NewBattleAreaDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Checking hit by enemy coordinates on logic layer.
        /// </summary>
        /// <param name="coordinatesOfHit">Enemy coordinates.</param>
        /// <returns></returns>
        public async Task<Result<Maybe<NewCoordinatesDto>>> CheckHitAsync(NewCoordinatesDto coordinatesOfHit)
        {
            try
            {
                var fleetModel = _mapper.Map<IEnumerable<NewCoordinatesDto>>(await _battleAreaContext.Coordinates
                    .AsNoTracking().Where(x => x.BattleAreaId == coordinatesOfHit.BattleAreaId).ToArrayAsync().ConfigureAwait(false));

                var attackedCell = fleetModel.SingleOrDefault(x => x.CoordinateX == coordinatesOfHit.CoordinateX
                    && x.CoordinateY == coordinatesOfHit.CoordinateY);

                if (attackedCell != null)
                {
                    attackedCell.IsDamaged = true;

                    CheckShip(fleetModel, attackedCell);

                    var dbModel = _mapper.Map<CoordinatesDb>(attackedCell);
                    _battleAreaContext.Coordinates.Attach(dbModel);
                    var entry = _battleAreaContext.Entry(dbModel);
                    entry.State = EntityState.Modified;
                }
                else
                {
                    _battleAreaContext.Coordinates.Add(_mapper.Map<CoordinatesDb>(coordinatesOfHit));
                }

                await _battleAreaContext.SaveChangesAsync().ConfigureAwait(false);

                Maybe<NewCoordinatesDto> result = _mapper.Map<NewCoordinatesDto>(attackedCell);
                return Result.Success(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.Warning(ex, "An error occurred while updating the model in the DB");
                return Result.Failure<Maybe<NewCoordinatesDto>>(ex.Message);
            }
        }

        private void CheckShip(IEnumerable<NewCoordinatesDto> fleetModel, NewCoordinatesDto attackedCell)
        {
            var shipCells = fleetModel.Where(x => x.ShipsId == attackedCell.ShipsId).ToArray();
            var alifeCells = shipCells.FirstOrDefault(x => x.IsDamaged == false);

            if (alifeCells == null)
            {
                SetEmptyCells(shipCells);
            }
        }

        private void SetEmptyCells(IEnumerable<NewCoordinatesDto> attackedShip) // TODO Logic for empty cells around downed ship
        {
            
        }
    }
}
