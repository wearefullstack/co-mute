namespace CoMute.Web.Migrations
{
    using CoMute.Web.Seed;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoMute.Web.Data.ComuteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoMute.Web.Data.ComuteContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            SeedData.Init(3);
            context.Users.AddRange(SeedData.Users);
            context.CarPools.AddRange(SeedData.CarPools);
            context.AvailableDays.AddRange(SeedData.AvailableDays);
            context.SaveChanges();
        }
    }
}
