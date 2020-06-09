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
        private readonly FleetContext _fleetContext;
        private readonly IMapper _mapper;

        public GameService([NotNull]FleetContext fleetContext,
            [NotNull]IMapper mapper)
        {
            this._fleetContext = fleetContext;
            this._mapper = mapper;
        }

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="fleetModel">Own fleet coordinates.</param>
        public Result<FleetDto> SetFleet(FleetDto fleetModel)
        {
            try
            {
                var dbFleetModel = _mapper.Map<FleetDb>(fleetModel);

                _fleetContext.Fleets.Add(dbFleetModel);
                _fleetContext.SaveChanges();

                fleetModel.FleetId = dbFleetModel.FleetId;
                return Result.Success(fleetModel);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<FleetDto>(ex.Message);
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
                var dbmodel = _fleetContext.Coordinates.Where(x => x.FleetId == coordinatesOfHit.FleetId)
                    .SingleOrDefault(c => c.CoordinateX == coordinatesOfHit.CoordinateX
                    && c.CoordinateY == coordinatesOfHit.CoordinateY);

                if (dbmodel != null)
                {
                    dbmodel.IsDamaged = true;
                    _fleetContext.SaveChanges();

                    return Result.Success(); 
                }
                return Result.Failure("not valid model");
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<CoordinatesDto>(ex.Message);
            }
        }
    }
}
