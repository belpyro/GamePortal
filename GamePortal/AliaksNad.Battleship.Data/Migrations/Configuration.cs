namespace AliaksNad.Battleship.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AliaksNad.Battleship.Data.Contexts.BattleAreaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AliaksNad.Battleship.Data.Contexts.BattleAreaContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Users.Add(new IdentityUser { UserName = "Evil", PasswordHash = new PasswordHasher().HashPassword("999"), Email = "Evil@example.com"});

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
