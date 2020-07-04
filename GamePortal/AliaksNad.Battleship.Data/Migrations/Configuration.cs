namespace AliaksNad.Battleship.Data.Migrations
{
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
            ContextKey = "AliaksNad.Battleship.Data.Contexts.BattleAreaContext";
        }

        protected override void Seed(AliaksNad.Battleship.Data.Contexts.BattleAreaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (context.Roles.Any()) return;

            context.Roles.Add(new IdentityRole("user"));
            context.SaveChanges();
        }
    }
}
