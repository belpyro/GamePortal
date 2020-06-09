using AliaksNad.Battleship.Data.Models;
using Serilog;
using System.Data.Entity;

namespace AliaksNad.Battleship.Data.Contexts
{
    public sealed class FleetContexts : DbContext
    {
        public FleetContexts(ILogger logger)
        {
            //Database.SetInitializer<UsersContexts>(new MigrateDatabaseToLatestVersion<UsersContexts, Configuration>());
            Database.SetInitializer<FleetContexts>(new DropCreateDatabaseAlways<FleetContexts>());
            Database.Log = msg => logger.Warning(msg);
        }

        public DbSet<FleetDb> FleetCoordinates { get; set; }

        public DbSet<CoordinatesDb> Coordinates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new FleetDbConfiguration());
        }
    }
}
