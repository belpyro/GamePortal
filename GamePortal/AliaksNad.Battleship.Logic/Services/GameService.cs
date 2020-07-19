using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using AliaksNad.Battleship.Logic.Services.Contracts;
using AutoMapper;
using CSharpFunctionalExtensions;
using Fody;
using JetBrains.Annotations;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Services
{
    [ConfigureAwait(false)]
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
        public async Task<Result<BattleAreaDto>> AddAsync(BattleAreaDto BattleAreaModel)
        {
            try
            {
                var dbBattleAreaModel = _mapper.Map<BattleAreaDb>(BattleAreaModel);

                _battleAreaContext.BattleAreas.Add(dbBattleAreaModel);
                await _battleAreaContext.SaveChangesAsync();

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
        public async Task<Result<IEnumerable<BattleAreaDto>>> GetAllAsync()
        {
            try
            {
                var models = await _battleAreaContext.BattleAreas.AsNoTracking().ProjectToArrayAsync<BattleAreaDto>(_mapper.ConfigurationProvider);
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
        public async Task<Result<Maybe<BattleAreaDto>>> GetByIdAsync(int id)
        {
            try
            {
                Maybe<BattleAreaDto> battleArea = await _battleAreaContext.BattleAreas.Where(x => x.BattleAreaId == id)
                    .ProjectToSingleOrDefaultAsync<BattleAreaDto>(_mapper.ConfigurationProvider);
                return Result.Success(battleArea);
            }
            catch (SqlException ex)
            {
                _logger.Warning(ex, "An error occurred while reading model by id from the DB");
                return Result.Failure<Maybe<BattleAreaDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Check enemy ship with target coordinates.
        /// </summary>
        /// <param name="coordinates">Enemy coordinates.</param>
        /// <returns></returns>
        public async Task<Result<Maybe<TargetDto>>> CheckTargetAsync(TargetDto target)
        {
            try
            {
                var targetCell = await CheckShipAsync(target);

                if (targetCell != null)
                {
                    await SaveDamagedShipAsync(targetCell);
                }
                else
                {
                    await SaveEmptyCellAsync(target);
                }

                Maybe<TargetDto> result = _mapper.Map<TargetDto>(targetCell);
                return Result.Success(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.Warning(ex, "An error occurred while updating the model in the DB");
                return Result.Failure<Maybe<TargetDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Delete battle area by id.
        /// </summary>
        /// <param name="id">BattleArea id.</param>
        /// <returns></returns>
        public async Task<Result> DeleteBattleAreaAsync (int id)
        {
            try
            {
                var result = await _battleAreaContext.BattleAreas.Include(x => x.Ships).Where(x => x.BattleAreaId == id)
                    .SingleOrDefaultAsync();

                if (result != null)
                {
                    _battleAreaContext.Ships.RemoveRange(result.Ships);
                    _battleAreaContext.BattleAreas.Remove(result);
                    _battleAreaContext.SaveChanges(); 

                    return Result.Success();
                }

                return Result.Failure("Battle area was not found.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<CoordinatesDb> CheckShipAsync(TargetDto target)
        {
            try
            {
                var fleetDb = await _battleAreaContext.Ships.AsNoTracking()
                    .Where(x => x.BattleAreaId == target.EnemyBattleAreaId && x.isAlife == true).Include(x => x.Coordinates).ToArrayAsync();

                return fleetDb.SelectMany(x => x.Coordinates)
                    .Where(x => x.CoordinateX == target.Coordinates.CoordinateX && x.CoordinateY == target.Coordinates.CoordinateY).SingleOrDefault();
            }
            catch (DbUpdateException ex)
            {
                throw new NotImplementedException(); // TODO
            }
        }

        private async Task SaveDamagedShipAsync(CoordinatesDb target)
        {
            try
            {
                target.IsDamage = true;

                _battleAreaContext.Coordinates.Attach(target);
                var entry = _battleAreaContext.Entry(target);
                entry.Property(s => s.IsDamage).IsModified = true;

                //CheckShip(fleetDb, targetFleetCell);  //TODO

                await _battleAreaContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Warning(ex, "An error occurred while updating the model in the DB");
            }
        }

        private async Task<Result> SaveDestroyedShipAsync(ShipDb ship) // TODO
        {
            try
            {
                int maxX = ship.Coordinates.Select(x => x.CoordinateX).Max() + 1;
                int minX = ship.Coordinates.Select(x => x.CoordinateX).Min() - 1;
                int maxY = ship.Coordinates.Select(x => x.CoordinateY).Max() + 1;
                int minY = ship.Coordinates.Select(x => x.CoordinateY).Min() - 1;

                var emptyCells = new EmptyCellDb();

                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        emptyCells.Coordinates.Add(new CoordinatesDb() { CoordinateX = x, CoordinatesId = y });
                    }
                }

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                _logger.Warning(ex, "An error occurred while updating the model in the DB");
                return Result.Failure<Maybe<CoordinatesDto>>(ex.Message);
            }
        }

        private async Task SaveEmptyCellAsync(TargetDto target)
        {
            try
            {
                var emptyCellsDb = await _battleAreaContext.EmptyCell.AsNoTracking()
                    .Where(x => x.BattleAreaId == target.EnemyBattleAreaId).SingleOrDefaultAsync();
                
                var targetDb = _mapper.Map<CoordinatesDb>(target.Coordinates);
                targetDb.EmptyCellId = emptyCellsDb.EmptyCellId;
                targetDb.IsDamage = true;

                _battleAreaContext.Coordinates.Add(targetDb);
                _battleAreaContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.Warning(ex, "An error occurred while updating the model in the DB");
            }
        }
    }
}
