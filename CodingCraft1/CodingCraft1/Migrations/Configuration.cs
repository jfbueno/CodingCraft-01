using System;
using CodingCraft1.Context;
using CodingCraft1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraft1.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingCraft1.Context.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MyContext context)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole {Name = "admin"});

            var user = new IdentityUser
            {
                UserName = "Jefh",
                Email = "jefhtavares@gmail.com",
                EmailConfirmed = true
            };


            userManager.Create(user, "123456");
            var adminUser = userManager.FindByName(user.UserName);
            userManager.AddToRole(adminUser.Id, "admin");
        }
    }
}
