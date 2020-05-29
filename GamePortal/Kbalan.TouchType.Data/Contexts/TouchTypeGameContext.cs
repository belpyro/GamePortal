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

            var Userentity = modelBuilder.Entity<UserDb>();
            var Textentity = modelBuilder.Entity<TextSetDb>();
            
            Textentity.HasKey(x => x.Id).ToTable("Text_sets");
            Userentity.HasKey(x => x.Id).ToTable("Users");

            Userentity.HasRequired(stat => stat.Statistic);
            Userentity.HasRequired(set => set.Setting);
            
            
              

          /*  Userentity.HasRequired(set => set.UserSetting)
                .WithRequiredPrincipal(op => op.User);*/

            
        }
    }
}
