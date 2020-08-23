namespace AliaksNad.Battleship.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmptyCell", "BattleAreaId", "dbo.BattleAreas");
            DropForeignKey("dbo.Coordinates", "EmptyCellId", "dbo.EmptyCell");
            DropForeignKey("dbo.Coordinates", "ShipId", "dbo.Ships");
            AddForeignKey("dbo.EmptyCell", "BattleAreaId", "dbo.BattleAreas", "BattleAreaId", cascadeDelete: true);
            AddForeignKey("dbo.Coordinates", "EmptyCellId", "dbo.EmptyCell", "EmptyCellId", cascadeDelete: true);
            AddForeignKey("dbo.Coordinates", "ShipId", "dbo.Ships", "ShipId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coordinates", "ShipId", "dbo.Ships");
            DropForeignKey("dbo.Coordinates", "EmptyCellId", "dbo.EmptyCell");
            DropForeignKey("dbo.EmptyCell", "BattleAreaId", "dbo.BattleAreas");
            AddForeignKey("dbo.Coordinates", "ShipId", "dbo.Ships", "ShipId");
            AddForeignKey("dbo.Coordinates", "EmptyCellId", "dbo.EmptyCell", "EmptyCellId");
            AddForeignKey("dbo.EmptyCell", "BattleAreaId", "dbo.BattleAreas", "BattleAreaId");
        }
    }
}
