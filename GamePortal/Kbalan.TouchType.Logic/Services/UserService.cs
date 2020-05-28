using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public UserService(TouchTypeGameContext gameContext, IMapper mapper)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
        }

        /// <summary>
        /// Implementation of IUserService GetAll() method
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RegisterUserDto> GetAll()
        {
            return _gameContext.Users.ProjectToArray<RegisterUserDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Implementation of IUserService GetById() method. If no user with such id exist's, null will be returned
        /// </summary>
        public RegisterUserDto GetById(int Id)
        {
            return _gameContext.Users.Where(x => x.Id == Id)
                .ProjectToSingleOrDefault<RegisterUserDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Implementation of IUserService GetByName() method. If no user with nickname exist's, null will be returned
        /// </summary>
        /// <param name="nickname">User nickname</param>
        /// <returns>User from RegisterUserDto collection or null</returns>
        public RegisterUserDto GetByName(string nickname)
        {
            return _gameContext.Users.Where(x => x.NickName == nickname)
                         .ProjectToSingleOrDefault<RegisterUserDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Add new user to RegisterUserDto collection. If user with such id or name exist's null will be
        /// returned
        /// </summary>
        /// <param name="model">RegisterUserDto model</param>
        /// <returns>New User or null</returns>
        public RegisterUserDto Add(RegisterUserDto model)
        {
            var DbModel = _mapper.Map<UserDb>(model);
            _gameContext.Users.Add(DbModel);
            _gameContext.SaveChanges();

            model.Id = DbModel.Id;
            return model;
        }

        /// <summary>
        /// Update existing user in RegisterUserDto collection by id. If no user's with such id found null will
        /// be returned
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="model">new RegisterUserId model</param>
        /// <returns>New User or null</returns>
/*        public void Update(RegisterUserDto model)
        {
            var dbModel = _gameContext.Users.Find(model.Id);
            
            dbModel.NickName = model.NickName;
            dbModel.Avatar = model.Avatar;
            dbModel.Email = model.Email;
            dbModel.LevelOfText = model.LevelOfText;
            dbModel.MaxSpeedRecord = model.MaxSpeedRecord;
            dbModel.NumberOfGamesPlayed = model.NumberOfGamesPlayed;
            dbModel.Password = dbModel.Password;
            dbModel.Role = dbModel.Role;

            _gameContext.SaveChanges();
        }*/

        /// <summary>
        /// Delete user in RegisterUserDto collection by id. If no user with such id found, false will be
        /// returned. If user deleted true will be returned.
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

        public void Update(RegisterUserDto model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
