using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.Data.Data
{
    public class SeedIdentityContext
    {
        public static async Task Initialize(
            IdentityContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<RecruitingUser> userManager, 
            string testUserPw)
        {
            dbContext.Database.EnsureCreated();

            if (!dbContext.Users.Any())
            {
                var adminID = await EnsureUser(userManager, testUserPw, "admin@recruiting.com", "Administrator", new DateTime(1980, 06, 23));
                await EnsureRole(userManager, roleManager, adminID, Roles.Administrator);
                var managerID = await EnsureUser(userManager, testUserPw, "manager@recruiting.com", "Manager", new DateTime(1964, 09, 04));
                await EnsureRole(userManager, roleManager, managerID, Roles.Manager);
                var recruiterID = await EnsureUser(userManager, testUserPw, "recruiter@recruiting.com", "Recruiter", new DateTime(1995, 03, 14));
                await EnsureRole(userManager, roleManager, recruiterID, Roles.Recruiter);
            }
        }

        private static async Task<string> EnsureUser(UserManager<RecruitingUser> userManager,
                                                    string testUserPw, string email, string fullName, DateTime birthdate)
        {
            var user = await userManager.FindByNameAsync(email);
            if (user == null)
            {
                user = new RecruitingUser
                {
                    UserName = email,
                    EmailConfirmed = true,
                    FullName = fullName,
                    Birthdate = birthdate
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(UserManager<RecruitingUser> userManager, 
                                                                RoleManager<IdentityRole> roleManager,
                                                                string uid, string role)
        {
            IdentityResult IR = null;

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
