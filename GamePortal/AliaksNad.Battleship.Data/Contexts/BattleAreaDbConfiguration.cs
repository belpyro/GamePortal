using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts
{
    internal class BattleAreaDbConfiguration : EntityTypeConfiguration<BattleAreaDb>
    {
        public BattleAreaDbConfiguration()
        {
            HasKey(x => x.BattleAreaId).ToTable("BattleAreas");
            HasMany(x => x.Ships).WithOptional(x => x.BattleArea).HasForeignKey(c => c.BattleAreaId);
            HasMany(x => x.FailedLaunch).WithOptional(x => x.BattleArea).HasForeignKey(c => c.BattleAreaId);
        }
    }
}