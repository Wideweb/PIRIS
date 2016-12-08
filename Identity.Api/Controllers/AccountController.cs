using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Core.Features.Finance.Services.Interfaces;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Task.FromResult(accountService.GetAccount(id)));
        }

        [HttpGet("GetAccountTransactionList")]
        public async Task<IActionResult> GetAccountTransactionList(int accountId)
        {
            return Ok(await Task.FromResult(accountService.GetAccountTransactionList(accountId)));
        }
    }
}
