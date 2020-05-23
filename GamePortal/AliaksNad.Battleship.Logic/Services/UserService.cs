using AliaksNad.Battleship.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Services
{
    internal class UserService : IUserService
    {
        IEnumerable<UserDto> _users = UserFaker.Generate();

        public IEnumerable<UserDto> GetAll()
        {
            return _users;
        }
    }
}
