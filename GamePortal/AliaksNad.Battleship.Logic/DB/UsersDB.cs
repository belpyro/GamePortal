using AliaksNad.Battleship.Logic.DB;
using AliaksNad.Battleship.Logic.Models;
using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.DB
{
    internal class UsersDB
    {
        internal static List<UserDto> _users = new List<UserDto>
        {
            new UserDto { Id = 1, Name = "Jeck", Password = "666", Email = "Jeck@"},
            new UserDto { Id = 2, Name = "Jesus", Password = "102", Email = "Jesus@"},
            new UserDto { Id = 3, Name = "SpanchBob", Password = "314", Email = "SpanchBob@"}
        };
    }
}