using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.Data;
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
    }
}
