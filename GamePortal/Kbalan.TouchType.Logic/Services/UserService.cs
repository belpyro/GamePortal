using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Fody;
using JetBrains.Annotations;
using Kbalan.Logic.Extensions;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    [ConfigureAwait(false)]
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            this._mapper = mapper;
        }

        public async Task<Result> Register(NewUserDto model)
        {
            // validation username existing
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            var result2 = await _userManager.AddToRoleAsync(user.Id, "user");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            await _userManager.SendEmailAsync(user.Id, "Confirm your email", $"click on https://localhost:44444/api/user/email/confirm?userId={user.Id}&token={token}");

            return Result.Combine(result.ToFunctionalResult(), result2.ToFunctionalResult());
        }

        public async Task<Result> ChangePassword(string userId, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(userId, token, newPassword);
            return result.ToFunctionalResult();
        }

        public async Task<Result> ConfirmEmail(string userId, string token)
        {
            var data = await _userManager.ConfirmEmailAsync(userId, token);
            return data.ToFunctionalResult();
        }

        public async Task<Result> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ValidationException("User doesn't exist");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            await _userManager.SendEmailAsync(user.Id, "Reset your password", $"Click on yourhost/api/users/password/reset?userId={user.Id}&token={token}");
            return Result.Success();
        }

        public async Task<Result<IReadOnlyCollection<UserDto>>> GetAllAsync()
        {
            //bad practice! don't use it in production. only as sample
            var users = await _userManager.Users.ProjectToListAsync<UserDto>(_mapper.ConfigurationProvider);
            return Result.Success((IReadOnlyCollection<UserDto>)users.AsReadOnly());
        }

        public async Task<Maybe<UserDto>> GetUser(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(user, password);
            return isValid ? _mapper.Map<UserDto>(user) : null;
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
