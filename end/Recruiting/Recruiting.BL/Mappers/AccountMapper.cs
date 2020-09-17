using Microsoft.AspNetCore.Identity;
using Recruiting.BL.Models;
using Recruiting.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Mappers
{
    public static class AccountMapper
    {
        public static async Task<List<Account>> MapEntityToDomain(List<RecruitingUser> users, UserManager<RecruitingUser> userManager)
        {
            List<Account> accounts = new List<Account>();
            foreach (RecruitingUser user in users)
            {
                Account account = new Account { FullName = user.FullName, Email = user.UserName, UserId = user.Id, BirthDate = user.Birthdate };
                account.Roles = await userManager.GetRolesAsync(user);
                accounts.Add(account);
            }
            return accounts;
        }
    }
}
