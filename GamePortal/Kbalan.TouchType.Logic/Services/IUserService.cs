using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;

namespace Kbalan.TouchType.Logic.Services
{
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Return all register user from RegisterUserDto
        /// </summary>
        /// <returns>All Registered user from RegisterUserDto</returns>
        IEnumerable<UserDto> GetAll();

        /// <summary>
        /// Returns registered user from RegisterUserDto collection by it's id. 
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Single user from RegisterUserDto or null</returns>
        UserDto GetById(int Id);

        /// <summary>
        /// Returns registered user from RegisterUserDto by it's name. 
        /// </summary>
        /// <param name="nickname">User nickname</param>
        /// <returns>Single user from RegisterUserDto or null</returns>
        UserDto GetByName(string nickname);

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