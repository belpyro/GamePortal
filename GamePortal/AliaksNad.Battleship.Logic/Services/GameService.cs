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
                var dbShipsModel = _battleAreaContext.Ships.Include(c => c.Ship).SingleOrDefault(x => x.BattleAreaId == coordinatesOfHit.BattleAreaId);
                var dbCoordinatesOfHit = dbShipsModel.Ship.SingleOrDefault(x => x.CoordinateX == coordinatesOfHit.CoordinateX && x.CoordinateY == coordinatesOfHit.CoordinateY);
                
                if (dbCoordinatesOfHit != null)
                {
                    var dbCoordinatesModel = _battleAreaContext.Coordinates.Where(x => x.BattleAreaId == coordinatesOfHit.BattleAreaId);

                    dbCoordinatesOfHit.IsDamaged = true;
                    CheckShip(dbCoordinatesModel, dbCoordinatesOfHit.ShipsId);
                    
                    _battleAreaContext.SaveChanges();
                }

                _battleAreaContext.Coordinates.Add(_mapper.Map<CoordinatesDb>(coordinatesOfHit));

                Maybe<CoordinatesDto> result = _mapper.Map<CoordinatesDto>(dbCoordinatesOfHit);
                return Result.Success(result);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<Maybe<CoordinatesDto>>(ex.Message);
            }
        }

        private void CheckShip(IEnumerable<CoordinatesDb> coordinates, int attackedShipId)
        {
            var attackedShip = coordinates.Where(x => x.ShipsId == attackedShipId); // TODO check implementation of variables
            var alifeCells = attackedShip.FirstOrDefault(x => x.IsDamaged == false);

            if (alifeCells != null)
            {
                SetEmptyCells(attackedShip);
            }
        }

        private void SetEmptyCells(IEnumerable<CoordinatesDb> attackedShip) // TODO Logic for empty cells around downed ship
        {
            
        }
    }
}
