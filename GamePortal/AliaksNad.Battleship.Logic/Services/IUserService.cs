using AliaksNad.Battleship.Logic.Models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Services
{
    public interface IUserService : IDisposable 
    {
        IEnumerable<UserDto> GetAll();

        UserDto GetById(int id);

        Result<UserDto> Add(UserDto model);

        void Update(UserDto model);

        void Delete(int id);
    }
}