using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts.Configurations
{
    internal class MissCellsConfiguration : EntityTypeConfiguration<MissCellDb>
    {
        public MissCellsConfiguration()
        {
            HasKey(x => x.MissCellId).ToTable("MissCells");
            HasMany(x => x.Coordinates).WithOptional(x => x.MissCellDb).HasForeignKey(x => x.MissCellId);
        }
    }
}