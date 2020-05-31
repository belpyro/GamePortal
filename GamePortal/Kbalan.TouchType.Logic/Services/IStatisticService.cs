using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    public interface IStatisticService
    {
        /// <summary>
        /// Return information about all statistic with it's user
        /// </summary>
        /// <returns>All user with statistic</returns>
        IEnumerable<UserStatisticDto> GetAll();

        /// <summary>
        /// Returns information about statistic's with it's user by user id
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Statistic with user</returns>
        UserStatisticDto GetById(int id);

        /// <summary>
        /// Updating existing user statistic
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        void Update(int id, StatisticDto model);
    }
}
