using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;

namespace Kbalan.TouchType.Logic.Services
{
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Return full information(with statistic and settings) about all register user from UserDto
        /// </summary>
        /// <returns>All Registered user from UserDto</returns>
        IEnumerable<UserDto> GetAll();



        /// <summary>
        /// Add new user to RegisterUserDto collection
        /// </summary>
        /// <param name="model">New user</param>
        /// <returns>New registered user or null</returns>
        UserDto Add(UserDto model);

        /// <summary>
        /// Updating existing user in RegisteredUserDto collection by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="model">New user model</param>
        /// <returns>New user or null</returns>
        void Update(UserDto model);

        /// <summary>
        /// Delete existing user in RegisterUserDto by it's id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>true of false</returns>
        void Delete(int id);
    }
}