using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Extensions;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Services.Contracts;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using Fody;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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

    [ConfigureAwait(false)]
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService([NotNull]IMapper mapper,
            [NotNull]UserManager<IdentityUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }

        /// <summary>
        /// Create and register new user in app
        /// </summary>
        /// <param name="model">New user model</param>
        /// <returns></returns>
        public async Task<Result> RegisterAsync(NewUserDto model)
        {
            var user = _mapper.Map<IdentityUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            var result2 = await _userManager.AddToRoleAsync(user.Id, "user");

            await SendEmailConfirmationTokenAsync(user.Id);

            return Result.Combine(result.ToFunctionalResult(), result2.ToFunctionalResult());
        }

        /// <summary>
        /// Create and register new user in app by OAuth 2.0
        /// </summary>
        /// <param name="model">OAuth 2.0 New user model</param>
        /// <returns></returns>
        public async Task<Result> RegisterExternalUserAsync(ExternalLoginInfo info)
        {
            var user = await _userManager.FindAsync(info.Login);
            if (user != null) return Result.Success();

            user = _mapper.Map<IdentityUser>(info);
            var result = await _userManager.CreateAsync(user);
            var result2 = await _userManager.AddToRoleAsync(user.Id, "user");
            await _userManager.AddLoginAsync(user.Id, info.Login);

            return Result.Combine(result.ToFunctionalResult(), result2.ToFunctionalResult());
        }

        /// <summary>
        /// Get user by name and password
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="password">User password</param>
        /// <returns></returns>
        public async Task<Maybe<UserDto>> GetUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(user, password);
            return isValid ? _mapper.Map<UserDto>(user) : null;
        }

        /// <summary>
        /// Reset user password in app
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        public async Task<Result> ResetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ValidationException("User doesn't exist");

            await SendPasswordResetTokenAsync(user.Id);
            return Result.Success();
        }

        /// <summary>
        /// Change user password in app
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="token">Validation token</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        public async Task<Result> ChangePasswordAsync(string userId, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(userId, token, newPassword);
            return result.ToFunctionalResult();
        }

        /// <summary>
        /// Confirm user email in app
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="token">Validation token</param>
        /// <returns></returns>
        public async Task<Result> ConfirmEmailAsync(string userId, string token)
        {
            var data = await _userManager.ConfirmEmailAsync(userId, token);
            return data.ToFunctionalResult();
        }

        /// <summary>
        /// Delete user in app
        /// </summary>
        /// <param name="userId">User ID</param>
        public async Task<Result> DeleteAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return Result.Failure("Didn't find user by this id");

                var result = await _userManager.DeleteAsync(user);
                return result.ToFunctionalResult();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<UserDto>(ex.Message);
            }
        }

        private async Task SendEmailConfirmationTokenAsync(string userId)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            await _userManager.SendEmailAsync(userId, "confirm your email", $"Click on https://localhost:55555/api/user/email/comfirm?userId={userId}&token={token}");
        }

        private async Task SendPasswordResetTokenAsync(string userId)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(userId);
            await _userManager.SendEmailAsync(userId, "Reset your password", $"Click on https://localhost:55555/api/users/password/reset?userId={userId}&token={token}");
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

                _userManager.Dispose();
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
