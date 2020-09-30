using Microsoft.AspNetCore.Identity;
using Recruiting.BL.Mappers;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<RecruitingUser> _userManager;
        private readonly SignInManager<RecruitingUser> _signInManager;
        private Func<List<RecruitingUser>, UserManager<RecruitingUser>, Task<List<Account>>> _mapListEntityToListDomain;
        private Func<RecruitingUser, UserManager<RecruitingUser>, Task<Account>> _mapEntityToDomain;

        public AccountService(UserManager<RecruitingUser> userManager,
                                SignInManager<RecruitingUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapListEntityToListDomain = AccountMapper.MapListEntitytoListDomain;
            _mapEntityToDomain = AccountMapper.MapEntityToDomain;
        }

        public async Task<Account> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return await _mapEntityToDomain(user, _userManager);
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
            =>
                await _mapListEntityToListDomain(_userManager.Users.ToList(), _userManager);

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Account> UpdateAsync(Account account)
        {
            var user = await _userManager.FindByIdAsync(account.UserId);

            user.UserName = account.Email;
            user.Email = account.Email;
            user.FullName = account.FullName;

            var result = await _userManager.UpdateAsync(user);
            return await _mapEntityToDomain(user, _userManager);
        }

        public async Task UpdateRolesAsync(Account account, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(account.UserId);
            var formerRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, formerRoles);
            await _userManager.AddToRolesAsync(user, roles);
        }
    }
}
