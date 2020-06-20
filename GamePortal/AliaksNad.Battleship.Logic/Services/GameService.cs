using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
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
        public Result<BattleAreaDto> Add(BattleAreaDto BattleAreaModel)
        {
            try
            {
                var dbBattleAreaModel = _mapper.Map<BattleAreaDb>(BattleAreaModel);

                _battleAreaContext.BattleAreas.Add(dbBattleAreaModel);
                _battleAreaContext.SaveChanges();

                BattleAreaModel.BattleAreaId = dbBattleAreaModel.BattleAreaId;
                return Result.Success(BattleAreaModel);
            }
            catch (DbUpdateException ex)
            {
                _logger.Warning(ex, "An error occurred while writing the model to the DB");
                return Result.Failure<BattleAreaDto>(ex.Message);
            }
        }

        /// <summary>
        /// Get all battle area from data.
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<BattleAreaDto>> GetAll()
        {
            try
            {
                var models = _battleAreaContext.BattleAreas.AsNoTracking().ToArray();
                return Result.Success(_mapper.Map<IEnumerable<BattleAreaDto>>(models));
            }
            catch (SqlException ex)
            {
                _logger.Warning(ex, "An error occurred while reading models from the DB");
                return Result.Failure<IEnumerable<BattleAreaDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Get battle area from data by id.
        /// </summary>
        /// <param name="id">battle area id.</param>
        /// <returns></returns>
        public Result<Maybe<BattleAreaDto>> GetById(int id)
        {
            try
            {
                Maybe<BattleAreaDto> battleArea = _battleAreaContext.BattleAreas.Where(x => x.BattleAreaId == id).ProjectToSingleOrDefault<BattleAreaDto>(_mapper.ConfigurationProvider);
                return Result.Success(battleArea);
            }
            catch (SqlException ex)
            {
                _logger.Warning(ex, "An error occurred while reading model by id from the DB");
                return Result.Failure<Maybe<BattleAreaDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Checking hit by enemy coordinates on logic layer.
        /// </summary>
        /// <param name="coordinatesOfHit">Enemy coordinates.</param>
        /// <returns></returns>
        public Result<Maybe<CoordinatesDto>> CheckHit(CoordinatesDto coordinatesOfHit)
        {
            try
            {
                var fleetModel = _mapper.Map<IEnumerable<CoordinatesDto>>(_battleAreaContext.Coordinates
                    .AsNoTracking().Where(x => x.BattleAreaId == coordinatesOfHit.BattleAreaId));

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

                _battleAreaContext.SaveChanges();

                Maybe<CoordinatesDto> result = _mapper.Map<CoordinatesDto>(attackedCell);
                return Result.Success(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.Warning(ex, "An error occurred while updating the model in the DB");
                return Result.Failure<Maybe<CoordinatesDto>>(ex.Message);
            }
        }

        private void CheckShip(IEnumerable<CoordinatesDto> fleetModel, CoordinatesDto attackedCell)
        {
            var shipCells = fleetModel.Where(x => x.ShipsId == attackedCell.ShipsId).ToArray();
            var alifeCells = shipCells.FirstOrDefault(x => x.IsDamaged == false);

            if (alifeCells == null)
            {
                SetEmptyCells(shipCells);
            }
        }

        private void SetEmptyCells(IEnumerable<CoordinatesDto> attackedShip) // TODO Logic for empty cells around downed ship
        {
            
        }
    }
}
