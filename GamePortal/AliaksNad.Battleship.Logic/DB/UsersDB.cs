using AliaksNad.Battleship.Logic.DB;
using AliaksNad.Battleship.Logic.Models;
using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.DB
{
    internal class UsersDB
    {
        internal static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Jeck", Password = "666", Email = "Jeck@"},
            new User { Id = 2, Name = "Jesus", Password = "102", Email = "Jesus@"},
            new User { Id = 3, Name = "SpanchBob", Password = "314", Email = "SpanchBob@"}
        };
    }
}