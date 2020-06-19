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
    /// Service for Statistic
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Return information about all statistic with it's user
        /// </summary>
        /// <returns>All user with statistic</returns>
        Result<IEnumerable<UserStatisticDto>> GetAll();
        Task<Result<IEnumerable<UserStatisticDto>>> GetAllAsync();

        /// <summary>
        /// Returns information about statistic's with it's user by user id
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <returns>Statistic with user</returns>
        Task<Result<Maybe<UserStatisticDto>>> GetByIdAsync(int id);
        Result<Maybe<UserStatisticDto>> GetById(int id);

        /// <summary>
        /// Updating existing user statistic
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        Task<Result> UpdateAsync(int id, StatisticDto model);
        Result Update(int id, StatisticDto model);
    }
}
