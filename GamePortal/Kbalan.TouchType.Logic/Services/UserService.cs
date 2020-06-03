using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    internal class UserService : IUserService
    {
        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;
        private readonly IValidator<UserSettingDto> _userSettingValidator;
        private readonly IValidator<UserDto> _userValidator;

        public UserService(TouchTypeGameContext gameContext, IMapper mapper, IValidator<UserSettingDto> UserSettintValidator, IValidator<UserDto> UserValidator)
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
        public IEnumerable<UserSettingStatisticDto> GetAll()
        {
            return _gameContext.Users.ProjectToArray<UserSettingStatisticDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Implementation of GetById()
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public UserSettingStatisticDto GetById(int id)
        {
            return _gameContext.Users.Where(x => x.Id == id)
                .ProjectToSingleOrDefault<UserSettingStatisticDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Add new user to RegisterUserDto collection. If user with such id or name exist's null will be
        /// returned
        /// </summary>
        /// <param name="model">RegisterUserDto model</param>
        /// <returns>New User or null</returns>
        public UserSettingDto Add(UserSettingDto model)
        {
            _userSettingValidator.ValidateAndThrow(model, "PostValidation");
            var DbModel = _mapper.Map<UserDb>(model);
            _gameContext.Users.Add(DbModel);
            _gameContext.SaveChanges();

            model.Id = DbModel.Id;
            return model;
        }

        /// <summary>
        /// Implementation of Update()
        /// </summary>
        /// <param name="model"></param>
        public void Update(UserDto model)
        {
            _userValidator.ValidateAndThrow(model, "PostValidation");
            var dbModel = _mapper.Map<UserDb>(model);
            _gameContext.Users.Attach(dbModel);
            var entry = _gameContext.Entry(dbModel);
            entry.Property(x => x.NickName).IsModified = true;
            entry.Property(x => x.Password).IsModified = true;
            
            _gameContext.SaveChanges();
        }

        /// <summary>
        /// Implementation of Delete()
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>true or false</returns>
        public void Delete(int id)
        {
                var dbModel = _gameContext.Users.Find(id);
                _gameContext.Users.Remove(dbModel);
                _gameContext.SaveChanges();
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
