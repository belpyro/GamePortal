using Igro.Quoridor.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Data.Contexts
{
    public sealed class UserContext : DbContext
    {
    public DbSet<UserDb> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entity = modelBuilder.Entity<UserDb>();
            entity.HasKey(x => x.Id).ToTable("BaseUsers");
            entity.Property(x => x.UserName).IsRequired().HasMaxLength(100)
                .IsUnicode().IsVariableLength();
            entity.Property(x => x.FirstName).HasMaxLength(40).IsUnicode()
                .IsVariableLength();
            entity.Property(x => x.LastName).HasMaxLength(40).IsUnicode()
                .IsVariableLength();
            entity.Property(x => x.Password).IsRequired().HasMaxLength(100).IsUnicode()
                .IsVariableLength();
            entity.Property(x => x.Email).IsRequired().HasMaxLength(100).IsUnicode()
                .IsVariableLength();
            entity.Property(x => x.DateOfBirth).IsRequired();
            entity.Property(x => x.Avatar).IsUnicode().IsVariableLength();
        }
    }
}
