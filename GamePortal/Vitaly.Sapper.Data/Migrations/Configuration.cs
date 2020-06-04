using System.Data.Entity.Migrations;
using Vitaly.Sapper.Data.Contexts;


namespace Vitaly.Sapper.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Vitaly.Sapper.Data.Contexts.SapperContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Vitaly.Sapper.Data.Contexts.SapperContext";
        }

        protected override void Seed(SapperContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
