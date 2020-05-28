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

        public DbSet<TextSetDb> TextSets { get; set; }

        public DbSet<UserDb> Users { get; set; }

        public DbSet<UserStatisticDb> UserStatistics { get; set; }

        public DbSet<UserSettingDb> UserSettings { get; set; }

        public TouchTypeGameContext() : base ("TouchTypeGameContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TouchTypeGameContext>());
            Database.Log = msg => Debug.WriteLine(msg);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var Userentity = modelBuilder.Entity<UserDb>();
            var Textentity = modelBuilder.Entity<TextSetDb>();
            var UserStatentity = modelBuilder.Entity<UserStatisticDb>();
            var UserSetentity = modelBuilder.Entity<UserSettingDb>();

            Userentity.HasKey(x => x.Id).ToTable("Users");
            UserStatentity.HasKey(x => x.Id).ToTable("UsersStatistic");
            UserSetentity.HasKey(x => x.Id).ToTable("UsersSettings");

            Userentity.HasOptional(set => set.UserStatistic)
                .WithOptionalDependent(op => op.User)
                .Map(m => m.MapKey("UserStatisticId"));

            Userentity.HasOptional(set => set.UserSetting)
                .WithOptionalDependent(op => op.User)
                .Map(m => m.MapKey("UserSettingId"));

            Userentity.Property(x => x.NickName).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();

           
            Textentity.HasKey(x => x.Id).ToTable("Text_sets");
        }
    }
}
