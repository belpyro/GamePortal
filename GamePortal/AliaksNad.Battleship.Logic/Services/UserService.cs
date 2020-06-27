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
        private readonly UsersContext _context;
        private readonly BattleAreaContext _areaContext;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService([NotNull]UsersContext context,
            [NotNull]BattleAreaContext areaContext,
            [NotNull]IMapper mapper,
            [NotNull]UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._areaContext = areaContext;
            this._mapper = mapper;
            this._userManager = userManager;
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

                //model.Id = dbModel.Id;
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

        public async Task<Result> Register(NewUserDto model)
        {
            // validation
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            var result2 = await _userManager.AddToRoleAsync(user.Id, "user");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            await _userManager.SendEmailAsync(user.Id, "confirm your email", $"Click on https://localhost:55555/api/user/email/comfirm?userId={user.Id}&token={token}");

            return Result.Combine(result.ToFunctionalResult(), result2.ToFunctionalResult());
        }

        public async Task<Result> RegisterExternalUser(ExternalLoginInfo info)
        {
            var user = await _userManager.FindAsync(info.Login);
            if (user != null) return Result.Success();

            user = new IdentityUser(info.Email) { Email = info.Email };
            await _userManager.CreateAsync(user);

            await _userManager.AddLoginAsync(user.Id, info.Login);
            return Result.Success();
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
