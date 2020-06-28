using AliaksNad.Battleship.Logic.Models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Services
{
    public interface IUserService : IDisposable 
    {
        /// <summary>
        /// Get all users. 
        /// </summary>
        Result<IEnumerable<UserDto>> GetAll();

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">user id.</param>
        Result<Maybe<UserDto>> GetById(int id);

        /// <summary>
        /// Add user to context.
        /// </summary>
        /// <param name="model">User model</param>
        Result<UserDto> Add(UserDto model);

        /// <summary>
        /// Update user model by id.
        /// </summary>
        /// <param name="model">User model</param>
        Result Update(UserDto model);

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="id">user id.</param>
        /// <returns></returns>
        Result Delete(int id);
    }
}