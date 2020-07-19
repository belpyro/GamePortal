using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts.Configurations
{
    internal class ShipsDbConfiguration : EntityTypeConfiguration<ShipDb>
    {
        public ShipsDbConfiguration()
        {
            HasKey(x => x.ShipId).ToTable("Ships");
            HasMany(x => x.Coordinates).WithOptional(x => x.ShipDb).HasForeignKey(x => x.ShipId).WillCascadeOnDelete();
        }
    }
}