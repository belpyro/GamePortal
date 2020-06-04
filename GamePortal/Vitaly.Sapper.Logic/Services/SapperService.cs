using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using Vitaly.Sapper.Data.Contexts;
using Vitaly.Sapper.Data.Models;
using Vitaly.Sapper.Logic.Models;

namespace Vitaly.Sapper.Logic.Services
{
    public class SapperService : ISapperService
    {
        private readonly SapperContext _context;
        private readonly IMapper _mapper;

        public SapperService(SapperContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            //return new List<UserDto>() { new UserDto() { Id = 222, Name = "aaa", Email = "1@1.com", Pwd = "gfgf" } };
            var models = _context.Users.AsNoTracking().ToArray();
            return _mapper.Map<IEnumerable<UserDto>>(models);
        }

        public UserDto UserInfoById(int id)
        {
            var dbModel = new UserDb {Id = id};
            _context.Users.Attach(dbModel);
            var entry = _context.Entry(dbModel);

            return _mapper.Map<UserDto>(entry.Entity);
        }

        public UserDto UserAdd(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto UserUpdate(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto UserDelete(int id)
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
        // ~PizzaService()
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
