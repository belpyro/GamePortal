using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
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

        public UserDto Add(UserDto model)
        {
            var dbModel = new UserDb
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                CreatorId = 666
            };

            _context.Users.Add(dbModel);
            _context.SaveChanges();

            model.Id = dbModel.Id;
            return model;
        }

        public UserDto GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateById()
        {
            throw new NotImplementedException();
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
