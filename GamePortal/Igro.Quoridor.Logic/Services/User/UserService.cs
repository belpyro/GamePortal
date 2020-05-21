using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using Igro.Quoridor.Logic.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamePortal.Logic.Igro.Quoridor.Logic.Services.User
{
    internal class UserService : IUserService
    {
        public static List<UserDTO> _users = UserFaker.GenerateList();

        public static Dictionary<UserDTO, int> _leaderBoard = new Dictionary<UserDTO, int>
        {
            [new UserDTO() { UserName = "Ivan" }] = 10,
            [new UserDTO() { UserName = "Fedor" }] = 5,
        };

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _users;
        }

        public UserDTO GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user;
        }
        public UserDTO EditProfileUsers(int id, UserDTO user)
        {
            var oldUser = _users.FirstOrDefault(x => x.Id == id);
            if (oldUser != null)
            {
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
            return user;
        }
        public bool DeleteProfileUsers(int id)
        {
            bool find = false;
            var user = _users.FirstOrDefault(x => x.Id == id);
            _users.Remove(user);
            return user != null ? find = true : find;
        }


        public (UserDTO, bool) RegisterNewPlayer(UserDTO regUser)
        {
            bool alreadyUse = false;
            var userEmail = _users.FirstOrDefault(x => x.Email == regUser.Email);
            if (userEmail == null)
            {
                var id = _users.Last().Id + 1;
                regUser.Id = id;
                _users.Add(regUser);
                return (regUser, alreadyUse);
            }
            else
            {
                alreadyUse = true;
                return (userEmail, alreadyUse);
            }
        }

        public bool LogIn(string email, string password)
        {
            bool find = false;
            var user = _users.FirstOrDefault(x => x.Password == password && x.Email == email);
            return user != null ? find = true : find;
        }
        public string RestorePassword(string email)
        {
            string password = "Not found";
            var findUser = _users.FirstOrDefault(x => x.Email == email);
            return findUser == null ? password : findUser.Password;
            
        }
    }
}