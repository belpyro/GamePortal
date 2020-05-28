using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private static IEnumerable<UserDto> _users = UserFaker.Generate();

        public UserService(UsersContexts context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get all users from Data.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDto> GetAll()
        {
            //return _users;

            //return _context.Users
            //    .Select(x => new UserDto {Id = x.Id, Name = x.Name, Email = x.Email, Password = x.Password })
            //    .ToArray();

            //var models = _context.Users.ToArray();
            //return _mapper.Map<IEnumerable<UserDto>>(models);

            return _context.Users.ProjectToArray<UserDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Add user to data.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns></returns>
        public UserDto Add(UserDto model)
        {
            //var dbModel = new UserDb
            //{
            //    Name = model.Name,
            //    Email = model.Email,
            //    Password = model.Password,
            //    CreatorId = 666
            //};

            var dbModel = _mapper.Map<UserDb>(model);

            _context.Users.Add(dbModel);
            _context.SaveChanges();

            model.Id = dbModel.Id;
            return model;
        }

        /// <summary>
        /// Get user from data by id.
        /// </summary>
        /// <param name="id">user id.</param>
        /// <returns></returns>
        public UserDto GetById(int id)
        {
            //return _users.FirstOrDefault(x => x.Id == id);

            //var model = _context.Users.SingleOrDefault(x => x.Id == id);
            //return new UserDto
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Email = model.Email,
            //    Password = model.Password
            //};

            return _context.Users.Where(x => x.Id == id).ProjectToSingleOrDefault<UserDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Update user model in data.
        /// </summary>
        /// <param name="model">User model.</param>
        public void Update(UserDto model)
        {
            //var dbModel = _context.Users.SingleOrDefault(x => x.Id == model.Id);
            //dbModel.Name = model.Name;
            //dbModel.Email = model.Email;
            //dbModel.Password = model.Password;

            var dbModel = _mapper.Map<UserDb>(model);
            _context.Users.Attach(dbModel);
            var entry = _context.Entry(dbModel);
            entry.State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }

        /// <summary>
        /// Delete user in data by id.
        /// </summary>
        /// <param name="id">User id.</param>
        public void Delete(int id)
        {
            var dbModel = _context.Users.SingleOrDefault(x => x.Id == id);
            _context.Users.Remove(dbModel);

            _context.SaveChanges();
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
