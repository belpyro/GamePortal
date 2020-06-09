namespace AliaksNad.Battleship.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatisticDbs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Score = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false),
                    UserDb_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.UserDb_Id)
                .Index(t => t.UserDb_Id);

            CreateTable(
                "dbo.Players",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 150),
                    Email = c.String(nullable: false, maxLength: 30),
                    Password = c.String(nullable: false, maxLength: 30),
                    CreatorId = c.Int(),
                    CreatedOn = c.DateTime(nullable: false),
                    UpdatedBy = c.Int(),
                    UpdatedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.StatisticDbs", "UserDb_Id", "dbo.Players");
            DropIndex("dbo.StatisticDbs", new[] { "UserDb_Id" });
            DropTable("dbo.Players");
            DropTable("dbo.StatisticDbs");
        }
    }
}
