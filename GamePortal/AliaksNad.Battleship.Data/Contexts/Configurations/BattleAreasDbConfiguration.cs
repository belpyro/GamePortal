using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts.Configurations
{
    internal class BattleAreasDbConfiguration : EntityTypeConfiguration<BattleAreaDb>
    {
        public BattleAreasDbConfiguration()
        {
            HasKey(x => x.BattleAreaId).ToTable("BattleAreas");
            HasMany(x => x.Ships).WithOptional(x => x.BattleAreas).HasForeignKey(c => c.BattleAreaId);
            HasMany(x => x.MissCells).WithOptional(x => x.BattleAreas).HasForeignKey(c => c.BattleAreaId);
        }
    }
}