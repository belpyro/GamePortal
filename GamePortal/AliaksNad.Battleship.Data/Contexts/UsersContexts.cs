using AliaksNad.Battleship.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Data.Contexts
{
    public sealed class UsersContexts : DbContext
    {
        public DbSet<UserDb> Users { get; set; }
    }
}
