using AutoMapper;
using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using Igro.Quoridor.Data.Contexts;
using Igro.Quoridor.Data.Models;
using Igro.Quoridor.Logic.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GamePortal.Logic.Igro.Quoridor.Logic.Services.User
{
    internal class UserService : IUserService
    {
        public static List<UserDTO> _users = UserFaker.GenerateList();
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public UserService(UserContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _context.Users.ProjectToArray<UserDTO>(_mapper.ConfigurationProvider);
        }

        public UserDTO GetById(int id)
        {
            return _context.Users.Where(x => x.Id == id).ProjectToSingleOrDefault<UserDTO>(_mapper.ConfigurationProvider);
        }
        public UserDTO EditProfileUsers(int id, UserDTO user)
        {
            var oldUser = _context.Users.Find(user.Id);

            oldUser.UserName = user.UserName;
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            oldUser.Password = user.Password;
            oldUser.Email = user.Email;
            oldUser.DateOfBirth = user.DateOfBirth;
            oldUser.Avatar = user.Avatar;
            _context.SaveChanges();
            return user;
        }
        public bool DeleteProfileUsers(int id)
        {
            bool find = false;
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return find = true;
            }
            else
            {
                return find;
            }

        }

        public (UserDTO, bool) RegisterNewPlayer(UserDTO regUser)
        {
            bool alreadyUse = false;
            var dbModel = _mapper.Map<UserDb>(regUser);
            _context.Users.Add(dbModel);
            _context.SaveChanges();
            regUser.Id = dbModel.Id;
            return (regUser, alreadyUse);
        }

        public bool LogIn(string email, string password)
        {
            bool find = false;
            var user = _context.Users.Where(x => x.Password == password && x.Email == email).ProjectToSingleOrDefault<UserDTO>(_mapper.ConfigurationProvider);
            return user != null ? find = true : find;
        }
        public string RestorePassword(string email)
        {
            string password = "Not found";
            var findUser = _context.Users.Where(x => x.Email == email).ProjectToSingleOrDefault<UserDTO>(_mapper.ConfigurationProvider);
            return findUser == null ? password : findUser.Password;

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                _context.Dispose();
                GC.SuppressFinalize(this);
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}