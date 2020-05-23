using AliaksNad.Battleship.Data.Contexts;
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
        private readonly UsersContexts _context;
        private static IEnumerable<UserDto> _users = UserFaker.Generate();

        public UserService(UsersContexts context)
        {
            this._context = context;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _users;
        }

        public UserDto GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateById()
        {
            throw new NotImplementedException();
        }
    }
}
