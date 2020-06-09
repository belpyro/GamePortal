using AliaksNad.Battleship.Data.Models;
using Serilog;
using System.Data.Entity;

namespace AliaksNad.Battleship.Data.Contexts
{
    public sealed class FleetContext : DbContext
    {
        public FleetContext()
        {
            //Database.SetInitializer<UsersContexts>(new MigrateDatabaseToLatestVersion<UsersContexts, Configuration>());
            Database.SetInitializer<FleetContext>(new DropCreateDatabaseAlways<FleetContext>());
            //Database.Log = msg => logger.Warning(msg);
        }

        public DbSet<FleetDb> Fleets { get; set; }

        public DbSet<CoordinatesDb> Coordinates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new FleetDbConfiguration());
        }
    }
}
