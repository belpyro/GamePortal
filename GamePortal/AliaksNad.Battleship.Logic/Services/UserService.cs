using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using JetBrains.Annotations;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly UsersContexts _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private static IEnumerable<UserDto> _users = UserFaker.Generate();

        public UserService([NotNull]UsersContexts context, 
                           [NotNull]IMapper mapper, 
                           [NotNull]ILogger logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Get all users from data.
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<UserDto>> GetAll()
        {
            try
            {
                //_logger.Warning("Get all users requested by anonymous"); //TODO: log
                var models = _context.Users.AsNoTracking().Include(x => x.Statistics).ToArray();
                return Result.Success(_mapper.Map<IEnumerable<UserDto>>(models));
            }
            catch (SqlException ex)
            {
                //_logger.Error("Connection to db is failed", ex); //TODO: log
                return Result.Failure<IEnumerable<UserDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Get user from data by id.
        /// </summary>
        /// <param name="id">user id.</param>
        /// <returns></returns>
        public Result<Maybe<UserDto>> GetById(int id)
        {
            try
            {
                Maybe<UserDto> user = _context.Users.Where(x => x.Id == id).ProjectToSingleOrDefault<UserDto>(_mapper.ConfigurationProvider);
                return Result.Success(user);
            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<UserDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Add user to data.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns></returns>
        public Result<UserDto> Add(UserDto model)
        {
            try
            {
                var dbModel = _mapper.Map<UserDb>(model);

                _context.Users.Add(dbModel);
                _context.SaveChanges();

                model.Id = dbModel.Id;
                return Result.Success(model);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<UserDto>(ex.Message);
            }
        }

        /// <summary>
        /// Update user model in data user id.
        /// </summary>
        /// <param name="model">User model.</param>
        public Result Update(UserDto model)
        {
            try
            {
                var dbModel = _mapper.Map<UserDb>(model);
                _context.Users.Attach(dbModel);
                var entry = _context.Entry(dbModel);
                entry.State = EntityState.Modified;

                _context.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<UserDto>(ex.Message);
            }
        }

        /// <summary>
        /// Delete user in data by id.
        /// </summary>
        /// <param name="id">User id.</param>
        public Result Delete(int id)
        {
            try
            {
                var dbModel = _context.Users.SingleOrDefault(x => x.Id == id);
                _context.Users.Remove(dbModel);

                _context.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<UserDto>(ex.Message);
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

                _context.Dispose();
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
