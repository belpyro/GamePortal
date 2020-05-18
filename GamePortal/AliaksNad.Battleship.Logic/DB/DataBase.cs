using AliaksNad.Battleship.Logic.DTO;
using AliaksNad.Battleship.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.DB
{
    public class DataBase
    {
        /// <summary>
        /// Returns list of all users from current DB.
        /// </summary>
        public List<User> GetUsers()
        {
            return UsersDB._users;
        }

        /// <summary>
        /// Update user from current DB.
        /// </summary>
        public void UpdateUser(User user)
        {
            var z = UsersDB._users.FindIndex(x => x.Id == user.Id);
            UsersDB._users[z] = user;
        }

        /// <summary>
        /// Returns list of all statistics from current DB.
        /// </summary>
        public List<StatisticDTO> GetStatistic()
        {
            return StatisticDB._statistics;
        }
    }
}
