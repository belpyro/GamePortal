using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts
{
    internal class ShipsDbConfiguration : EntityTypeConfiguration<ShipDb>
    {
        public ShipsDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("Ships");
            HasMany(x => x.ShipCoordinates).WithOptional(x => x.Ships).HasForeignKey(x => x.ShipsId);
        }
    }
}