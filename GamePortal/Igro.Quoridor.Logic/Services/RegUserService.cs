using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using Igro.Quoridor.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamePortal.Logic.Igro.Quoridor.Logic.Services
{
    internal class RegUserService : IRegUserService
    {
        public static List<RegPlayerDTO> _users = RegUserFaker.GenerateList();

        public static Dictionary<RegPlayerDTO, int> _leaderBoard = new Dictionary<RegPlayerDTO, int>
        {
            [new RegPlayerDTO() { UserName = "Ivan" }] = 10,
            [new RegPlayerDTO() { UserName = "Fedor" }] = 5,
        };

        public IEnumerable<RegPlayerDTO> GetAllUsers()
        {
            return _users;
        }

        public RegPlayerDTO GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user;
        }
        public RegPlayerDTO EditProfileUsers(int id, RegPlayerDTO user)
        {
            var oldUser = _users.FirstOrDefault(x => x.Id == id);
            oldUser.Id = user.Id;
            oldUser.UserName = user.UserName;
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            oldUser.Password = user.Password;
            oldUser.Email = user.Email;
            oldUser.DateOfBirth = user.DateOfBirth;
            oldUser.Avatar = user.Avatar;
            return user;
        }
        public bool DeleteProfileUsers(int id)
        {
            bool find = false;
            var user = _users.FirstOrDefault(x => x.Id == id);
            _users.Remove(user);
            return user != null ? find = true : find;
        }


        public RegPlayerDTO RegisterNewPlayer(RegPlayerDTO regUser)
        {
            var id = _users.Last().Id + 1;
            regUser.Id = id;
            _users.Add(regUser);
            return regUser;
        }

        public bool LogIn(string email, string password)
        {
            bool find = false;
            var user = _users.FirstOrDefault(x => x.Password == password && x.Email == email);
            return user != null ? find = true : find;
        }
        public string RestorePassword(string email)
        {
            string password =_users.FirstOrDefault(x => x.Email == email).Password;
            return password;
            
        }
    }
}