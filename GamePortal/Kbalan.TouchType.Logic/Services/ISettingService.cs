using CSharpFunctionalExtensions;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    /// <summary>
    /// Service for Setting
    /// </summary>
    public interface ISettingService
    {
        /// <summary>
        /// Return information about all settings with it's user
        /// </summary>
        /// <returns>All user with setting</returns>
        Result<IEnumerable<UserSettingDto>> GetAll();
        Task<Result<IEnumerable<UserSettingDto>>> GetAllAsync();

        /// <summary>
        /// Returns information about setting's with it's user by user id
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Setting with user</returns>
        Result<Maybe<UserSettingDto>> GetById(string id);
        Task<Result<Maybe<UserSettingDto>>> GetByIdAsync(string id);

        /// <summary>
        /// Updating existing user setting
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        Result Update(string id, SettingDto model);
        Task<Result> UpdateAsync(SettingDto model);
    }
}

