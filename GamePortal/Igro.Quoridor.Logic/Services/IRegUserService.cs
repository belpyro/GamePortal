using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Logic.Services
{
    public interface IRegUserService
    {
        IEnumerable<RegPlayerDTO> GetAllUsers();
        RegPlayerDTO GetById(int id);
        RegPlayerDTO EditProfileUsers(int id, RegPlayerDTO user);
        bool DeleteProfileUsers(int id);
        RegPlayerDTO RegisterNewPlayer(RegPlayerDTO regUser);
        bool LogIn(string email, string password);
        string RestorePassword(string email);
    }
}
