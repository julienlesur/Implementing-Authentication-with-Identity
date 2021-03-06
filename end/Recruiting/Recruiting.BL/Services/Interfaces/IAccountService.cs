﻿using Recruiting.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAccountsAsync();
        public Task<Account> FindByIdAsync(string id);
        public Task<Account> UpdateAsync(Account account);
        public Task UpdateRolesAsync(Account account, List<string> roles);
        Task Logout();
    }
}
