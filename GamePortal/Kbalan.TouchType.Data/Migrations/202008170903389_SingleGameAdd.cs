namespace Kbalan.TouchType.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SingleGameAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SingleGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        TextForTyping = c.String(),
                        CurrentPartToType = c.String(),
                        SymbolsToType = c.Int(nullable: false),
                        SymbolsTyped = c.Int(nullable: false),
                        IsGameFinished = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SingleGames");
        }
    }
}
