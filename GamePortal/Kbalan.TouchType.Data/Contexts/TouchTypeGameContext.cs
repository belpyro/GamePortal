using Kbalan.TouchType.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbalan.TouchType.Data.Migrations;
using Serilog;

namespace Kbalan.TouchType.Data.Contexts
{
    /// <summary>
    /// Context for TouchTypeGame
    /// </summary>
    public sealed class TouchTypeGameContext : DbContext
    {
        public TouchTypeGameContext() : base ("TouchTypeGameContext")
        {
            Database.SetInitializer<TouchTypeGameContext>(new MigrateDatabaseToLatestVersion<TouchTypeGameContext, Configuration>());
            Database.Log = msg => Debug.WriteLine(msg);
        }

        public DbSet<TextSetDb> TextSets { get; set; }

        public DbSet<UserDb> Users { get; set; }

        public DbSet<StatisticDb> Statistics { get; set; }

        public DbSet<SettingDb> Setting { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(TouchTypeGameContext).Assembly);
        }
    }
}
