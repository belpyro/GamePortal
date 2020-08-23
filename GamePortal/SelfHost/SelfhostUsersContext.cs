using Microsoft.AspNet.Identity.EntityFramework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.IdentityServer3.SelfHost
{
    public sealed class SelfhostUsersContext : IdentityDbContext
    {
        public SelfhostUsersContext()
        {
        }

        public SelfhostUsersContext(ILogger logger)
        {
            Database.SetInitializer<SelfhostUsersContext>(new CreateDatabaseIfNotExists<SelfhostUsersContext>());
            Database.Log = msg => logger.Debug(msg);
        }

        public DbSet<ClaimsDb> Claims { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserDbConfiguration());
        }
    }

    public class ClaimsDb
    {
        public string ClaimsId { get; set; }
    }

    internal class UserDbConfiguration : EntityTypeConfiguration<ClaimsDb>
    {
        public UserDbConfiguration()
        {
            HasKey(x => x.ClaimsId).ToTable("Users");
        }
    }
}
