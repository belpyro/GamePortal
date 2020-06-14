using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;
        private readonly IValidator<UserSettingDto> _userSettingValidator;
        private readonly IValidator<UserDto> _userValidator;

        public UserService([NotNull]TouchTypeGameContext gameContext, [NotNull]IMapper mapper
            , [NotNull]IValidator<UserSettingDto> UserSettintValidator, [NotNull]IValidator<UserDto> UserValidator)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
            this._userSettingValidator = UserSettintValidator;
            this._userValidator = UserValidator;
         
        }

        /// <summary>
        /// Implementation of IUserService GetAll() method
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<UserSettingStatisticDto>> GetAll()
        {
            try
            {
                var getAllResult = _gameContext.Users.ProjectToArray<UserSettingStatisticDto>(_mapper.ConfigurationProvider);
                return Result.Success<IEnumerable<UserSettingStatisticDto>>(getAllResult);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<IEnumerable<UserSettingStatisticDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Implementation of GetById()
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Result<UserSettingStatisticDto> GetById(int id)
        {
            try
            {
                var getResultById = _gameContext.Users.Where(x => x.Id == id)
                    .ProjectToSingleOrDefault<UserSettingStatisticDto>(_mapper.ConfigurationProvider);

                if (getResultById != null)
                    return Result.Success<UserSettingStatisticDto>(getResultById);

                return Result.Failure<UserSettingStatisticDto>("No user with such id exist");
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<UserSettingStatisticDto>(ex.Message);
            }
        }

        /// <summary>
        /// Add new user to RegisterUserDto collection. If user with such id or name exist's null will be
        /// returned
        /// </summary>
        /// <param name="model">RegisterUserDto model</param>
        /// <returns>New User or null</returns>
        public Result<UserSettingDto> Add(UserSettingDto model)
        {
            try
            {
                var DbModel = _mapper.Map<UserDb>(model);

                _gameContext.Users.Add(DbModel);
                _gameContext.SaveChanges();

                model.Id = DbModel.Id;
                return Result.Success(model);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<UserSettingDto>(ex.Message);
            }
        }

        /// <summary>
        /// Implementation of Update()
        /// </summary>
        /// <param name="model"></param>
        public Result Update(UserDto model)
        {

            try
            {
                var dbModel = _mapper.Map<UserDb>(model);

                _gameContext.Users.Attach(dbModel);

                var entry = _gameContext.Entry(dbModel);
                entry.Property(x => x.NickName).IsModified = true;
                entry.Property(x => x.Password).IsModified = true;
                _gameContext.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Implementation of Delete()
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>true or false</returns>
        public Result Delete(int id)
        {
            try
            {
                var dbModel = _gameContext.Users.Find(id);

                if (dbModel == null)
                    return Result.Failure($"No user with id {id} exist");

                _gameContext.Users.Remove(dbModel);
                _gameContext.SaveChanges();
                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public IValidator<UserDto> UserValidator { get; }

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
