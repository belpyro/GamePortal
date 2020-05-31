using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    public interface ISettingService
    {
        /// <summary>
        /// Return information about all settings with it's user
        /// </summary>
        /// <returns>All user with setting</returns>
        IEnumerable<UserSettingDto> GetAll();

        /// <summary>
        /// Returns information about setting's with it's user by user id
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Setting with user</returns>
        UserSettingDto GetById(int id);

        /// <summary>
        /// Updating existing user setting
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        void Update(int id, SettingDto model);

    }
}
