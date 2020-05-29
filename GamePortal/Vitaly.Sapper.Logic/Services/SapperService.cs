using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vitaly.Sapper.Data.Contexts;
using Vitaly.Sapper.Logic.Models;

namespace Vitaly.Sapper.Logic.Services
{
    internal class SapperService : ISapperService
    {
        private readonly SapperContext _context;

        public SapperService(SapperContext context)
        {
            this._context = context;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return new List<UserDto>() { new UserDto() { Id = 222, Name = "aaa", Email = "1@1.com", Pwd = "gfgf" } };
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
