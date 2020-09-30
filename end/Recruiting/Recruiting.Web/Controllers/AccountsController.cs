using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.Data;
using System;
using System.Linq;
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
            if (Account.IsEmpty(account))
            {
                return RedirectToAction(nameof(AccountNotFound));
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, [Bind("UserId, FullName, Email, Birthdate")] Account account, string roles, bool rolesChanged)
        {
            if (ModelState.IsValid)
            {
                var updatedApplicant = await _accountService.UpdateAsync(account);
                if (Account.IsEmpty(updatedApplicant))
                {
                    return RedirectToAction(nameof(AccountNotFound));
                }

                if (User.IsInRole(Roles.Administrator))
                {
                    if (rolesChanged)
                    {
                        await _accountService.UpdateRolesAsync(account, roles.Split(",").ToList());
                    }
                    return RedirectToAction("List", "Accounts");
                }
                TempData["Message"] = "The profil has been succesfully saved";

                return RedirectToAction("Edit", "Accounts", new { id = account.UserId});
            }
            return View(account);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccountNotFound()
        {
            return View();
        }
    }
}
