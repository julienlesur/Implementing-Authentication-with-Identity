﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.Data;
using System;
using System.Threading.Tasks;

namespace Recruiting.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var accounts = await _accountService.GetAccountsAsync();

            return View(accounts);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var account = await _accountService.FindByIdAsync(id);
            if (account == null)
            {
                return RedirectToAction(nameof(AccountNotFound));
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, [Bind("UserId, FullName, Email, Birthdate, Roles")] Account account)
        {
            if (ModelState.IsValid)
            {
                var updatedApplicant = await _accountService.UpdateAsync(account);
                return RedirectToAction("Index", "Home");
            }
            return View(account);
        }

        public IActionResult AccountNotFound()
        {
            return View();
        }
    }
}
