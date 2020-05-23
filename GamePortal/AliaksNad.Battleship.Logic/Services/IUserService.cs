using AliaksNad.Battleship.Logic.Models;
using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Services
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();

        UserDto GetById(int id);

        void UpdateById();
    }
}