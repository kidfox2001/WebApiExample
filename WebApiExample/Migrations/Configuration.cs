namespace WebApiExample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ReactAuthentication.API;
    using WebApiExample.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApiExample.AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebApiExample.AuthContext";
        }

        protected override void Seed(WebApiExample.AuthContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Clients.AddOrUpdate(new Client()
            {
                Id = "reactApp",
                Secret = Helper.GetHash("ben123456"),
                Name = "ReactAuthentication",
                Active = true,
                RefreshTokenLifeTime = 60 * 24 * 10, // 10 days
                AllowedOrigin = "*"
            });
        }
    }
}
