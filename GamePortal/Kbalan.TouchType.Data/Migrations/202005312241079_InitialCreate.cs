namespace Kbalan.TouchType.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        SettingId = c.Int(nullable: false),
                        Email = c.String(),
                        Avatar = c.String(),
                        LevelOfText = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SettingId)
                .ForeignKey("dbo.User", t => t.SettingId, cascadeDelete: true)
                .Index(t => t.SettingId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NickName = c.String(),
                        Password = c.String(),
                        CreatorId = c.Int(),
                        CreateOn = c.DateTime(nullable: false),
                        UpdateBy = c.Int(),
                        UpdateOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistic",
                c => new
                    {
                        StatisticId = c.Int(nullable: false),
                        MaxSpeedRecord = c.Int(nullable: false),
                        NumberOfGamesPlayed = c.Int(nullable: false),
                        AvarageSpeed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatisticId)
                .ForeignKey("dbo.User", t => t.StatisticId, cascadeDelete: true)
                .Index(t => t.StatisticId);
            
            CreateTable(
                "dbo.Text_set",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
            DropForeignKey("dbo.Statistic", "StatisticId", "dbo.User");
            DropForeignKey("dbo.Setting", "SettingId", "dbo.User");
            DropIndex("dbo.Statistic", new[] { "StatisticId" });
            DropIndex("dbo.Setting", new[] { "SettingId" });
            DropTable("dbo.Text_set");
            DropTable("dbo.Statistic");
            DropTable("dbo.User");
            DropTable("dbo.Setting");
        }
    }
}
