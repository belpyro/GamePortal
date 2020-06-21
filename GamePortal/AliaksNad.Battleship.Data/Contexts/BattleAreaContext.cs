using AliaksNad.Battleship.Data.Models;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Serilog;
using System.Data.Entity;
using System.Reflection;

namespace AliaksNad.Battleship.Data.Contexts
{
    public sealed class BattleAreaContext : IdentityDbContext
    {
        public BattleAreaContext()
        {
        }

        public BattleAreaContext([NotNull]ILogger logger)
        {
            //Database.SetInitializer<UsersContexts>(new MigrateDatabaseToLatestVersion<UsersContexts, Configuration>());
            Database.SetInitializer<BattleAreaContext>(new CreateDatabaseIfNotExists<BattleAreaContext>());
            Database.Log = msg => logger.Debug(msg);
        }

        public DbSet<BattleAreaDb> BattleAreas { get; set; }

        public DbSet<ShipDb> Ships { get; set; }

        public DbSet<CoordinatesDb> Coordinates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
