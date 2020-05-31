using Kbalan.TouchType.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Contexts
{
    public sealed class TouchTypeGameContext : DbContext
    {

       // public DbSet<UserSettingDb> UserSettings { get; set; }

        public TouchTypeGameContext() : base ("TouchTypeGameContext")
        {
            //Database.SetInitializer<TouchTypeGameContext>(new DropCreateDatabaseAlways<TouchTypeGameContext>());
            Database.Log = msg => Debug.WriteLine(msg);
        }

        public DbSet<TextSetDb> TextSets { get; set; }

        public DbSet<UserDb> Users { get; set; }

        public DbSet<StatisticDb> Statistics { get; set; }

        public DbSet<SettingDb> Setting { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var UserEntity = modelBuilder.Entity<UserDb>();
            var TextEntity = modelBuilder.Entity<TextSetDb>();
            var StatisticEntity = modelBuilder.Entity<StatisticDb>();
            var SettingEntity = modelBuilder.Entity<SettingDb>();
            
            TextEntity.HasKey(x => x.Id).ToTable("Text_set");
            UserEntity.HasKey(x => x.Id).ToTable("User");
            StatisticEntity.HasKey(x => x.StatisticId).ToTable("Statistic");
            SettingEntity.HasKey(x => x.SettingId).ToTable("Setting");

            UserEntity.HasRequired(stat => stat.Statistic).WithRequiredPrincipal(us => us.User);
            UserEntity.HasRequired(set => set.Setting).WithRequiredPrincipal(us => us.User);
            
        }
    }
}
