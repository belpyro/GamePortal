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
        public IEnumerable<UserDto> GetAll()
        {
            return _gameContext.Users.ProjectToArray<UserDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Implementation of GetById()
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public UserDto GetAllById(int Id)
        {
            return _gameContext.Users.Where(x => x.Id == Id)
                .ProjectToSingleOrDefault<UserDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Add new user to RegisterUserDto collection. If user with such id or name exist's null will be
        /// returned
        /// </summary>
        /// <param name="model">RegisterUserDto model</param>
        /// <returns>New User or null</returns>
        public UserDto Add(UserDto model)
        {
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
            var dbModel = _mapper.Map<UserDb>(model);
            _gameContext.Users.Attach(dbModel);
            var entry = _gameContext.Entry(dbModel);
            entry.Property(x => x.NickName).IsModified = true;
            entry.Property(x => x.Password).IsModified = true;
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
