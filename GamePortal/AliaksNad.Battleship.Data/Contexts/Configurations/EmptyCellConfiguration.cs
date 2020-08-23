using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts.Configurations
{
    internal class EmptyCellConfiguration : EntityTypeConfiguration<EmptyCellDb>
    {
        public EmptyCellConfiguration()
        {
            HasKey(x => x.EmptyCellId).ToTable("EmptyCell");
            HasMany(x => x.Coordinates).WithOptional(x => x.EmptyCellDb).HasForeignKey(x => x.EmptyCellId).WillCascadeOnDelete();
        }
    }
}