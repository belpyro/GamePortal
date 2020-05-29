using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitaly.Sapper.Data.Migrations;
using Vitaly.Sapper.Data.Models;

namespace Vitaly.Sapper.Data.Contexts
{
    public sealed class SapperContext : DbContext
    {
        public SapperContext()
        {
            Database.SetInitializer<SapperContext>(new MigrateDatabaseToLatestVersion<SapperContext, Configuration>());
            Database.Log = msg => Debug.WriteLine(msg);
        }

        public DbSet<UserDb> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
