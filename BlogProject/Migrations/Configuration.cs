namespace BlogProject.Migrations
{
    using BlogProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BlogProject.Models.ApplicationDbContext";
        }

        protected override void Seed(BlogProject.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //creates manager roles
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            //Are there any roles in the r database equal to admin/guest/member, if not create admin/guest/member role
            #region Roles
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Guest"))
            {
                roleManager.Create(new IdentityRole { Name = "Guest" });
            }

            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            } 
            #endregion
            //Create user roles
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //Are there any users in the r database equal to admin/guest/member, if not create admin/guest/member user
            #region users
            if (!context.Users.Any(r => r.UserName == "Jecool17@gmail.com"))
            {
                ApplicationUser adminUser = new ApplicationUser()
                {
                    UserName = "Jecool17@gmail.com",
                    Email = "Jecool17@gmail.com",
                };

                userManager.Create(adminUser, "jeco4097");
            }

            if (!context.Users.Any(r => r.UserName == "Guest@myblog.com"))
            {
                ApplicationUser guestUser = new ApplicationUser()
                {
                    UserName = "Guest@myblog.com",
                    Email = "Guest@myblog.com",
                };

                userManager.Create(guestUser, "P@ssw0rd");
            }

            if (!context.Users.Any(r => r.UserName == "Member@myblog.com"))
            {
                ApplicationUser memberUser = new ApplicationUser()
                {
                    UserName = "Member@myblog.com",
                    Email = "Member@myblog.com",
                };

                userManager.Create(memberUser, "P@ssw0rd");
            }
            #endregion
            //if email is equal to application user then  assign role to user ID
            #region User ID initialization
            ApplicationUser admU = context.Users.FirstOrDefault(r => r.Email == "Jecool17@gmail.com");
            if (admU != null)
            {
                userManager.AddToRole(admU.Id, "Admin");
            }


            ApplicationUser GuestU = context.Users.FirstOrDefault(r => r.Email == "Guest@myblog.com");
            if (admU != null)
            {
                userManager.AddToRole(admU.Id, "Guest");
            }


            ApplicationUser MemberU = context.Users.FirstOrDefault(r => r.Email == "Member@myblog.com");
            if (admU != null)
            {
                userManager.AddToRole(admU.Id, "Member");
            } 
            #endregion







        }
    }
}