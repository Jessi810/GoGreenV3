namespace GoGreenV3.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<GoGreenV3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GoGreenV3.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var role = new IdentityRole { Name = "Developer" };
                manager.Create(role);

                role = new IdentityRole { Name = "Superuser" };
                manager.Create(role);

                role = new IdentityRole { Name = "Admin" };
                manager.Create(role);

                role = new IdentityRole { Name = "Moderator" };
                manager.Create(role);

                role = new IdentityRole { Name = "Operator" };
                manager.Create(role);

                role = new IdentityRole { Name = "Agent" };
                manager.Create(role);

                role = new IdentityRole { Name = "Rescuer" };
                manager.Create(role);

                role = new IdentityRole { Name = "Default" };
                manager.Create(role);
            }

            // Creates a default admin
            if (!(context.Users.Any(u => u.UserName == "admin@gogreen.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var defaultAdmin = new ApplicationUser
                {
                    UserName = "admin@gogreen.com",
                    Email = "admin@gogreen.com",
                    FirstName = "Default",
                    LastName = "Admin",
                    MemberSince = DateTime.Now,
                    LastActive = DateTime.Now,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };
                userManager.Create(defaultAdmin, "Admin-0");
                userManager.AddToRole(defaultAdmin.Id, "Admin");
            }
        }
    }
}
