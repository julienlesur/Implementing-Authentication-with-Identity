﻿using Recruiting.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAccountsAsync();
    }
}