using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts.Configurations
{
    internal class CoordinatesDbConfiguration : EntityTypeConfiguration<CoordinatesDb>
    {
        public CoordinatesDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("Coordinates");
        }
    }
}