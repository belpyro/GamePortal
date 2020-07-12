namespace AliaksNad.Battleship.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BattleAreas",
                c => new
                    {
                        BattleAreaId = c.Int(nullable: false, identity: true),
                        CreatorId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BattleAreaId);
            
            CreateTable(
                "dbo.MissCells",
                c => new
                    {
                        MissCellId = c.Int(nullable: false, identity: true),
                        BattleAreaId = c.Int(),
                    })
                .PrimaryKey(t => t.MissCellId)
                .ForeignKey("dbo.BattleAreas", t => t.BattleAreaId)
                .Index(t => t.BattleAreaId);
            
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        CoordinatesId = c.Int(nullable: false, identity: true),
                        CoordinateX = c.Int(nullable: false),
                        CoordinateY = c.Int(nullable: false),
                        IsDamage = c.Boolean(nullable: false),
                        ShipId = c.Int(),
                        MissCellId = c.Int(),
                    })
                .PrimaryKey(t => t.CoordinatesId)
                .ForeignKey("dbo.Ships", t => t.ShipId)
                .ForeignKey("dbo.MissCells", t => t.MissCellId)
                .Index(t => t.ShipId)
                .Index(t => t.MissCellId);
            
            CreateTable(
                "dbo.Ships",
                c => new
                    {
                        ShipId = c.Int(nullable: false, identity: true),
                        isAlife = c.Boolean(nullable: false),
                        BattleAreaId = c.Int(),
                    })
                .PrimaryKey(t => t.ShipId)
                .ForeignKey("dbo.BattleAreas", t => t.BattleAreaId)
                .Index(t => t.BattleAreaId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ships", "BattleAreaId", "dbo.BattleAreas");
            DropForeignKey("dbo.MissCells", "BattleAreaId", "dbo.BattleAreas");
            DropForeignKey("dbo.Coordinates", "MissCellId", "dbo.MissCells");
            DropForeignKey("dbo.Coordinates", "ShipId", "dbo.Ships");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ships", new[] { "BattleAreaId" });
            DropIndex("dbo.Coordinates", new[] { "MissCellId" });
            DropIndex("dbo.Coordinates", new[] { "ShipId" });
            DropIndex("dbo.MissCells", new[] { "BattleAreaId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ships");
            DropTable("dbo.Coordinates");
            DropTable("dbo.MissCells");
            DropTable("dbo.BattleAreas");
        }
    }
}
