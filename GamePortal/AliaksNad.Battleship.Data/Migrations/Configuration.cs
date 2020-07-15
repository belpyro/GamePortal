namespace AliaksNad.Battleship.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.BattleAreaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexts.BattleAreaContext context)
        {
            //  This method will be called after migrating to the latest version.
            var cnf = new SeedConfig();
            //context.Users.AddOrUpdate(cnf.GetExampleUser());

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
