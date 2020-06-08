using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using JetBrains.Annotations;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;

namespace AliaksNad.Battleship.Logic.Services
{
    public class GameService : IGameService 
    {
        private static IEnumerable<Coordinates> _enemyFleet;
        private readonly UsersContexts _userContext;
        private readonly FleetContexts _fleetContexts;
        private readonly IMapper _mapper;

        public GameService([NotNull]UsersContexts userContext,
            [NotNull]FleetContexts fleetContexts,
            [NotNull]IMapper mapper)
        {
            this._userContext = userContext;
            this._fleetContexts = fleetContexts;
            this._mapper = mapper;
        }

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="fleetCoordinates">Own fleet coordinates.</param>
        public Result SetFleet(FleetDto fleetCoordinates)
        {
            //_enemyFleet = fleetCoordinates;

            try
            {
                var dbModel = _mapper.Map<FleetDb>(fleetCoordinates);

                _fleetContexts.FleetCoordinates.Add(dbModel);
                _
                _fleetContexts.SaveChanges();

                //_userContext.Coordinates.Add(dbModel);
                //_userContext.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<Coordinates>(ex.Message);
            }
        }

        /// <summary>
        /// Checking hit by enemy coordinates on logic layer.
        /// </summary>
        /// <param name="fleetCoordinates">Enemy coordinates.</param>
        /// <returns></returns>
        public bool CheckHit(Coordinates fleetCoordinates)
        {
            if (_enemyFleet == null)
                throw new NullReferenceException();
            
            foreach (var item in _enemyFleet)
            {
                if (item.Equals(fleetCoordinates))
                    return true;
            }

            return false;
        }
    }
}
