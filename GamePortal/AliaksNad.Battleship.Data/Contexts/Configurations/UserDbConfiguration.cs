using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts.Configurations
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
}