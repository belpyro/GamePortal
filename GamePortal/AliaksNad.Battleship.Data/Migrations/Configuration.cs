namespace AliaksNad.Battleship.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AliaksNad.Battleship.Data.Contexts.UsersContexts>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AliaksNad.Battleship.Data.Contexts.UsersContexts";
        }

        protected override void Seed(AliaksNad.Battleship.Data.Contexts.UsersContexts context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
