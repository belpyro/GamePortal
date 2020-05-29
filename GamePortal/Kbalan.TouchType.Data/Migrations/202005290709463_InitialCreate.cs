namespace Kbalan.TouchType.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SettingDbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Avatar = c.String(),
                        Password = c.String(),
                        LevelOfText = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NickName = c.String(),
                        CreatorId = c.Int(),
                        CreateOn = c.DateTime(nullable: false),
                        UpdateBy = c.Int(),
                        UpdateOn = c.DateTime(),
                        Statistic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SettingDbs", t => t.Id)
                .ForeignKey("dbo.StatisticDbs", t => t.Statistic_Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Statistic_Id);
            
            CreateTable(
                "dbo.StatisticDbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaxSpeedRecord = c.Int(nullable: false),
                        NumberOfGamesPlayed = c.Int(nullable: false),
                        AvarageSpeed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Text_sets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TextForTyping = c.String(),
                        LevelOfText = c.Int(nullable: false),
                        CreatorId = c.Int(),
                        CreateOn = c.DateTime(nullable: false),
                        UpdateBy = c.Int(),
                        UpdateOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Statistic_Id", "dbo.StatisticDbs");
            DropForeignKey("dbo.Users", "Id", "dbo.SettingDbs");
            DropIndex("dbo.Users", new[] { "Statistic_Id" });
            DropIndex("dbo.Users", new[] { "Id" });
            DropTable("dbo.Text_sets");
            DropTable("dbo.StatisticDbs");
            DropTable("dbo.Users");
            DropTable("dbo.SettingDbs");
        }
    }
}
