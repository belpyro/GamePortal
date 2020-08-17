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
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kbalan.TouchType.Data.Contexts
{
    /// <summary>
    /// Context for TouchTypeGame
    /// </summary>
    public sealed class TouchTypeGameContext : IdentityDbContext
    {
        public TouchTypeGameContext() : base ("TouchTypeGameContext")
        {
            Database.SetInitializer<TouchTypeGameContext>(new MigrateDatabaseToLatestVersion<TouchTypeGameContext, Configuration>());
            Database.Log = msg => Debug.WriteLine(msg);
        }

        public DbSet<TextSetDb> TextSets { get; set; }

        public DbSet<StatisticDb> Statistics { get; set; }

        public DbSet<SettingDb> Setting { get; set; }

       public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<SingleGame> SingleGames { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(TouchTypeGameContext).Assembly);
        }
    }
}
