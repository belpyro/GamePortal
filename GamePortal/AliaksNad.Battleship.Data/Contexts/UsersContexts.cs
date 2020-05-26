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
            Database.SetInitializer<UsersContexts>(new MigrateDatabaseToLatestVersion<UsersContexts, Configuration>());
            Database.Log = msg => Debug.WriteLine(msg);
        }

        public DbSet<UserDb> Users { get; set; }

        public DbSet<StatisticDb> Statistics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entity = modelBuilder.Entity<UserDb>();

            entity.HasKey(x => x.Id).ToTable("Players");
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            entity.Property(x => x.Password).IsRequired().HasMaxLength(30).IsUnicode().IsVariableLength();
            entity.Property(x => x.Email).IsRequired().HasMaxLength(30).IsUnicode().IsVariableLength();
            entity.HasMany(x => x.Statistics);
        }
    }
}
