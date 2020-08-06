using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Validators;
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
    public class SettingService : ISettingService
    {
        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;

        public SettingService([NotNull]TouchTypeGameContext gameContext, [NotNull]IMapper mapper)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
        }

        /// <summary>
        /// Return all User with settings from Db
        /// </summary>
        /// <returns>All Users with settings</returns>
        public Result<IEnumerable<UserSettingDto>> GetAll()
        {
            try
            {
                var models = _gameContext.ApplicationUsers.Include("Setting").ToArray();
                return Result.Success<IEnumerable<UserSettingDto>>(_mapper.Map<IEnumerable<UserSettingDto>>(models));
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<IEnumerable<UserSettingDto>>(ex.Message);
            }
        }
        public async Task<Result<IEnumerable<UserSettingDto>>> GetAllAsync()
        {
            try
            {
                var models = await _gameContext.ApplicationUsers.Include("Setting").ToArrayAsync().ConfigureAwait(false);
                return Result.Success<IEnumerable<UserSettingDto>>(_mapper.Map<IEnumerable<UserSettingDto>>(models));
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<IEnumerable<UserSettingDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Return User with it's setting by user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user with setting</returns>
        public Result<Maybe<UserSettingDto>> GetById(string id)
        {
            try
            {
                Maybe<UserSettingDto> getResultById = _gameContext.ApplicationUsers.Where(x => x.Id == id)
                    .ProjectToSingleOrDefault<UserSettingDto>(_mapper.ConfigurationProvider);

                    return Result.Success(getResultById);
            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<UserSettingDto>>(ex.Message);
            }
        }
        public async Task<Result<Maybe<UserSettingDto>>> GetByIdAsync(string id)
        {
            try
            {
                Maybe<UserSettingDto> getResultById = await _gameContext.ApplicationUsers.Where(x => x.Id == id)
                    .ProjectToSingleOrDefaultAsync<UserSettingDto>(_mapper.ConfigurationProvider)
                    .ConfigureAwait(false);

                return Result.Success(getResultById);
            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<UserSettingDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Update existing user setting
        /// </summary>
        /// <param name="id">user's id</param>
        /// <param name="model">new settting model</param>
        public Result Update(string id, SettingDto model)
        {
            //Cheking if user with id exist
            var userModel = _gameContext.ApplicationUsers.Include("Setting").SingleOrDefault(x => x.Id == id);

            //Replace model setting id from Dto to correct id from Db and Valiate
            model.SettingId = userModel.Setting.SettingId;

            try
            {
                var modelDb = _mapper.Map<SettingDb>(model);

                modelDb.SettingId = userModel.Setting.SettingId;
                modelDb.AppUser = userModel.Setting.AppUser;
                userModel.Setting = modelDb;

                _gameContext.Users.Attach(userModel);
                _gameContext.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
        public async Task<Result> UpdateAsync(string id, SettingDto model)
        {
            //Cheking if user with id exist
            var userModel = await _gameContext.ApplicationUsers.Include("Setting").SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            //Replace model setting id from Dto to correct id from Db and Valiate
            model.SettingId = userModel.Setting.SettingId;

            try
            {
                var modelDb = _mapper.Map<SettingDb>(model);

                modelDb.SettingId = userModel.Setting.SettingId;
                modelDb.AppUser = userModel.Setting.AppUser;
                userModel.Setting = modelDb;

                _gameContext.Users.Attach(userModel);
                await _gameContext.SaveChangesAsync();

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
