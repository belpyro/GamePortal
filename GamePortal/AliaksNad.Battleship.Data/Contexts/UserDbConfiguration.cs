 using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts
{
    internal class UserDbConfiguration : EntityTypeConfiguration<UserDb>
    {
        public UserDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("Players");
            Property(x => x.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            Property(x => x.Password).IsRequired().HasMaxLength(30).IsUnicode().IsVariableLength();
            Property(x => x.Email).IsRequired().HasMaxLength(30).IsUnicode().IsVariableLength();
            HasMany(x => x.Statistics).WithOptional();
        }
    }

    internal class FleetDbConfiguration : EntityTypeConfiguration<FleetDb>
    {
        public FleetDbConfiguration()
        {
            HasKey(x => x.FleetId).ToTable("Fleets");
            Property(x => x.Name).IsRequired().HasMaxLength(30);
            HasMany(x => x.Coordinates).WithOptional(x => x.Fleet).HasForeignKey(c => c.FleetId);
        }
    }
}