using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;

        public StatisticService([NotNull]TouchTypeGameContext gameContext, [NotNull]IMapper mapper)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
        }

        /// <summary>
        /// Return all User with statistic from Db
        /// </summary>
        /// <returns>All Users with statistic</returns>
        public Result<IEnumerable<UserStatisticDto>> GetAll()
        {
            try
            {
                var models = _gameContext.ApplicationUsers.Include("Statistic").ToArray();
                return Result.Success<IEnumerable<UserStatisticDto>>(_mapper.Map<IEnumerable<UserStatisticDto>>(models));
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<IEnumerable<UserStatisticDto>>(ex.Message);
            }
        }
        public async Task<Result<IEnumerable<UserStatisticDto>>> GetAllAsync()
        {
            try
            {
                var models = await _gameContext.ApplicationUsers.Include("Statistic").ToArrayAsync().ConfigureAwait(false);
                return Result.Success<IEnumerable<UserStatisticDto>>(_mapper.Map<IEnumerable<UserStatisticDto>>(models));
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<IEnumerable<UserStatisticDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Return User with it's statistic by user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user with statistic</returns>
        public Result<Maybe<UserStatisticDto>> GetById(string id)
        {
            try
            {
                Maybe<UserStatisticDto> getResultById = _gameContext.ApplicationUsers.Where(x => x.Id == id)
                    .ProjectToSingleOrDefault<UserStatisticDto>(_mapper.ConfigurationProvider);

                    return Result.Success(getResultById);

            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<UserStatisticDto>>(ex.Message);
            }
        }
        public async Task<Result<Maybe<UserStatisticDto>>> GetByIdAsync(string id)
        {
            try
            {
                Maybe<UserStatisticDto> getResultById = await _gameContext.ApplicationUsers.Where(x => x.Id == id)
                    .ProjectToSingleOrDefaultAsync<UserStatisticDto>(_mapper.ConfigurationProvider).ConfigureAwait(false);

                return Result.Success(getResultById);

            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<UserStatisticDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Update existing user statistic
        /// </summary>
        /// <param name="id">user's id</param>
        /// <param name="model">new statistic model</param>
        public Result Update(string id, StatisticDto model)
        {
            //Cheking if user with id exist
            var userModel = _gameContext.ApplicationUsers.Include("Statistic").SingleOrDefault(x => x.Id == id);

            //Replace model statistic id from Dto to correct id from Db and Valiate
            model.StatisticId = userModel.Statistic.StatisticId;
            try
            {
                var modelDb = _mapper.Map<StatisticDb>(model);

                modelDb.StatisticId = userModel.Statistic.StatisticId;
                modelDb.AppUser = userModel.Statistic.AppUser;
                userModel.Statistic = modelDb;

                _gameContext.Users.Attach(userModel);
                _gameContext.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
        public async Task<Result> UpdateAsync(StatisticDto model)
        {
            //Cheking if user with id exist
            var userModel = await _gameContext.ApplicationUsers.Include("Statistic").SingleOrDefaultAsync(x => x.Id == model.StatisticId)
                .ConfigureAwait(false);

            //Replace model statistic id from Dto to correct id from Db and Valiate
            model.StatisticId = userModel.Statistic.StatisticId;
            try
            {

                var modelDb = _mapper.Map<StatisticDb>(model);

                modelDb.StatisticId = userModel.Statistic.StatisticId;
                modelDb.AppUser = userModel.Statistic.AppUser;
                userModel.Statistic = modelDb;

                _gameContext.Users.Attach(userModel);
                await _gameContext.SaveChangesAsync().ConfigureAwait(false);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                _gameContext.Dispose();
                GC.SuppressFinalize(this);
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
