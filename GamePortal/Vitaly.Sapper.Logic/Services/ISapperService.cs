using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitaly.Sapper.Logic.Models;

namespace Vitaly.Sapper.Logic.Services
{
    public interface ISapperService : IDisposable
    {
        IEnumerable<UserDto> GetAll();
    }
}
