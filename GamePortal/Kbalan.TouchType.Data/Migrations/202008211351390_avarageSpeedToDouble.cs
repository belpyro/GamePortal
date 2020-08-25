namespace Kbalan.TouchType.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avarageSpeedToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Statistic", "AvarageSpeed", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Statistic", "AvarageSpeed", c => c.Int(nullable: false));
        }
    }
}
