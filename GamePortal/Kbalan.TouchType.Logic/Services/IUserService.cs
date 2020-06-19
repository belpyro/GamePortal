using CSharpFunctionalExtensions;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;

namespace Kbalan.TouchType.Logic.Services
{
    /// <summary>
    /// Service for User
    /// </summary>
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Return full information(with statistic and settings) about all users
        /// </summary>
        /// <returns>All users</returns>
        Result<IEnumerable<UserSettingStatisticDto>> GetAll();

        /// <summary>
        /// Return full information(with statistic and settings) about user by id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user</returns>
        Result<Maybe<UserSettingStatisticDto>> GetById(int id);

        /// <summary>
        /// Add new user 
        /// </summary>
        /// <param name="model">New user</param>
        /// <returns>New registered user</returns>
        Result<UserSettingDto> Add(UserSettingDto model);

        /// <summary>
        /// Updating existing user by id
        /// </summary>
        /// <param name="model">New user model</param>
        Result Update(UserDto model);

        /// <summary>
        /// Delete existing user by it's id
        /// </summary>
        /// <param name="id">User id</param>
        Result Delete(int id);
    }
}