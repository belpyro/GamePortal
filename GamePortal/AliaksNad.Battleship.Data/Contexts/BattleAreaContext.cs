using AliaksNad.Battleship.Data.Models;
using Serilog;
using System.Data.Entity;
using System.Reflection;

namespace AliaksNad.Battleship.Data.Contexts
{
    public sealed class BattleAreaContext : DbContext
    {
        public BattleAreaContext()
        {
            //Database.SetInitializer<UsersContexts>(new MigrateDatabaseToLatestVersion<UsersContexts, Configuration>());
            Database.SetInitializer<BattleAreaContext>(new CreateDatabaseIfNotExists<BattleAreaContext>());
            //Database.Log = msg => logger.Warning(msg);
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
