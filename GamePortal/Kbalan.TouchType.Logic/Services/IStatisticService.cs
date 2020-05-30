using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    interface IStatisticService
    {
        /// <summary>
        /// Return information about all statistics with it's user
        /// </summary>
        /// <returns>All user with statistics</returns>
        IEnumerable<StatisticDto> GetAll();

        /// <summary>
        /// Returns information about statistic with it's user by user id
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Statistic with userl</returns>
        StatisticDto GetById(int id);

        /// <summary>
        /// Updating existing user statistic
        /// </summary>
        /// <param name="model">New statistic model</param>
        /// <returns></returns>
        void Update(StatisticDto model);
    }
}
