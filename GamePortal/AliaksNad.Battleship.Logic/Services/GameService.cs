using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using JetBrains.Annotations;
using Serilog;
using System;
using System.Collections.Generic;
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

        public GameService([NotNull]BattleAreaContext _battleAreaContext,
            [NotNull]IMapper mapper)
        {
            this._battleAreaContext = _battleAreaContext;
            this._mapper = mapper;
        }

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="BattleAreaModel">Own fleet coordinates.</param>
        public Result<BattleAreaDto> SetFleet(BattleAreaDto BattleAreaModel)
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
                return Result.Failure<BattleAreaDto>(ex.Message);
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
