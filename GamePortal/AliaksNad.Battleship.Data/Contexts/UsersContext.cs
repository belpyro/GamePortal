﻿using AliaksNad.Battleship.Data.Contexts.Configurations;
using AliaksNad.Battleship.Data.Migrations;
using AliaksNad.Battleship.Data.Models;
using JetBrains.Annotations;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Data.Contexts
{
    public sealed class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext([NotNull]ILogger logger)
        {
            //Database.SetInitializer<UsersContexts>(new MigrateDatabaseToLatestVersion<UsersContexts, Configuration>());
            Database.SetInitializer<UsersContext>(new DropCreateDatabaseAlways<UsersContext>());
            Database.Log = msg => logger.Debug(msg);
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
