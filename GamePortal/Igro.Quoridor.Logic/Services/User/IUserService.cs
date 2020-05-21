using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Logic.Services.User
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetById(int id);
        UserDTO EditProfileUsers(int id, UserDTO user);
        bool DeleteProfileUsers(int id);
        (UserDTO, bool) RegisterNewPlayer(UserDTO regUser);
        bool LogIn(string email, string password);
        string RestorePassword(string email);
    }
}
