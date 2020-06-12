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
        public Result CheckHit(CoordinatesDto coordinatesOfHit)
        {
            try
            {
                //var dbmodel = _fleetContext.Coordinates.Where(x => x.FleetId == coordinatesOfHit.FleetId)
                //    .SingleOrDefault(c => c.CoordinateX == coordinatesOfHit.CoordinateX
                //    && c.CoordinateY == coordinatesOfHit.CoordinateY);

                //if (dbmodel != null)
                //{
                //    dbmodel.IsDamaged = true;
                //    _fleetContext.SaveChanges();

                //    return Result.Success(); 
                //}
                //return Result.Failure("not valid model");

                var dbBattleAreaModel = _battleAreaContext.BattleAreas.SingleOrDefault(x => x.BattleAreaId == coordinatesOfHit.BattleAreaId);

                var CoordinatesDto = dbBattleAreaModel.Ships.Add(x => x.Ship.SingleOrDefault(c => c.CoordinateX == coordinatesOfHit.CoordinateX && c.CoordinateY == coordinatesOfHit.CoordinateY));

                var dbFleetModel = _mapper.Map<CoordinatesDb>(coordinatesOfHit);
                _battleAreaContext.Ships.Attach(dbFleetModel);
                var entry = _battleAreaContext.Entry(dbFleetModel);
                entry.State = EntityState.Modified;

                _battleAreaContext.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<CoordinatesDto>(ex.Message);
            }
        }
    }
}
