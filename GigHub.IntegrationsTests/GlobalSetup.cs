using GigHub.Core.Models;
using GigHub.Migrations;
using GigHub.Persistance;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationsTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();

            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
            {
                return;
            }

            context.Users.Add(new ApplicationUser() { UserName = "user1", Name = "User1", Email = "what@ever.com", PasswordHash = "-" });
            context.Users.Add(new ApplicationUser() { UserName = "user2", Name = "User2", Email = "what@ever2.com", PasswordHash = "-" });

            context.SaveChanges();
        }
    }
}
