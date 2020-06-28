using AliaksNad.Battleship.Logic.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Services.Contracts
{
    public interface IUserService : IDisposable 
    {
        /// <summary>
        /// Create and register new user in app
        /// </summary>
        /// <param name="model">New user model</param>
        /// <returns></returns>
        Task<Result> RegisterAsync(NewUserDto model);

        /// <summary>
        /// Create and register new user in app by OAuth 2.0
        /// </summary>
        /// <param name="model">OAuth 2.0 New user model</param>
        /// <returns></returns>
        Task<Result> RegisterExternalUserAsync(ExternalLoginInfo info);

        /// <summary>
        /// Get user by name and password
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="password">User password</param>
        /// <returns></returns>
        Task<Maybe<UserDto>> GetUserAsync(string username, string password);

        /// <summary>
        /// Reset user password in app
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        Task<Result> ResetPasswordAsync(string email);

        /// <summary>
        /// Change user password in app
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="token">Validation token</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        Task<Result> ChangePasswordAsync(string userId, string token, string newPassword);

        /// <summary>
        /// Confirm user email in app
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="token">Validation token</param>
        /// <returns></returns>
        Task<Result> ConfirmEmailAsync(string userId, string token);

        /// <summary>
        /// Delete user in app
        /// </summary>
        /// <param name="userId">User ID</param>
        Task<Result> DeleteAsync(string userId);
    }
}