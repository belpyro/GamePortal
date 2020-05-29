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
        /// Return full information(with statistic and settings) about all register user from UserDto
        /// </summary>
        /// <returns>All Registered user from UserDto</returns>
        IEnumerable<UserDto> GetAllFull();

        /// <summary>
        /// Return all registered user with settings from UserDb
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserDto> GetAllWithSettings();

        /// <summary>
        /// Return all registered user with statistic from UserDb
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserDto> GetAllWithStatistic();

        /// <summary>
        /// Returns full info about registered user from RegisterUserDto collection by it's id. 
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Single user from RegisterUserDto or null</returns>
        UserDto GetFullById(int Id);

        /// <summary>
        /// Returns registered user with Statistics from RegisterUserDto collection by it's id. 
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Single user from RegisterUserDto or null</returns>
        UserDto GetStatById(int Id);

        /// <summary>
        /// Returns registered user  with Settings from RegisterUserDto collection by it's id. 
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Single user from RegisterUserDto or null</returns>
        UserDto GetSetById(int Id);

        /// <summary>
        /// Returns full info about registered user from RegisterUserDto collection by it's id. 
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Single user from RegisterUserDto or null</returns>
        UserDto GetFullByNick(string nick);

        /// <summary>
        /// Returns registered user with Statistics from RegisterUserDto collection by it's id. 
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Single user from RegisterUserDto or null</returns>
        UserDto GetStatByNick(string nick);

        /// <summary>
        /// Returns registered user  with Settings from RegisterUserDto collection by it's id. 
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Single user from RegisterUserDto or null</returns>
        UserDto GetSetByNick(string nick);

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