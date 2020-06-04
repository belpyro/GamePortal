using AliaksNad.Battleship.Data.Migrations;
using AliaksNad.Battleship.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Data.Contexts
{
    public sealed class UsersContexts : DbContext
    {
        public UsersContexts()
        {
            //Database.SetInitializer<UsersContexts>(new MigrateDatabaseToLatestVersion<UsersContexts, Configuration>());
            Database.SetInitializer<UsersContexts>(new DropCreateDatabaseAlways<UsersContexts>());
            Database.Log = msg => Debug.WriteLine(msg);
        }

        public DbSet<UserDb> Users { get; set; }

        public DbSet<StatisticDb> Statistics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserDbConfiguration());
        }
    }
}
