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
        }

        protected override void Seed(AliaksNad.Battleship.Data.Contexts.BattleAreaContext context)
        {
            //  This method will be called after migrating to the latest version.

            var cnf = new SeedConfig();
            var user = cnf.GetExampleUser();
            var result = context.Users.Where(x => x.UserName == user.UserName).SingleOrDefault();

            if (result == null)
            {
                context.Users.Add(user); 
            }

            if (context.Roles.Any()) return;

            context.Roles.Add(new IdentityRole("user"));
            context.SaveChanges();

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
