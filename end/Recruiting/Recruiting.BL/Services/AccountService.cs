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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<RecruitingUser> _userManager;
        private Func<List<RecruitingUser>, UserManager<RecruitingUser>, Task<List<Account>>> _mapListEntityToListDomain;
        private Func<RecruitingUser, UserManager<RecruitingUser>, Task<Account>> _mapEntityToDomain;

        public AccountService(RoleManager<IdentityRole> roleManager, UserManager<RecruitingUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        public Task<Account> UpdateAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
