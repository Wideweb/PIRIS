using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Features.Finance.Models;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class ATMController : Controller
    {
        private readonly IClientAccountService clientAccountService;

        public ATMController(IClientAccountService clientAccountService)
        {
            this.clientAccountService = clientAccountService;
        }
        
        [HttpGet("GetAccountBalance")]
        public async Task<IActionResult> GetAccountBalance([FromQuery] ATMGetAccountBalanceModel model)
        {
            return Ok(await Task.FromResult(clientAccountService
                .GetAccountBalanceByCreditCardNumber(model.CreditCardNumber)));
        }

        [HttpPost("WithdrawCash")]
        public async Task<IActionResult> WithdrawCash([FromBody] ATMWithdrawCashModel model)
        {
            return Ok(await clientAccountService.WithdrawCash(model));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] ATMLoginModel model)
        {
            return Ok(await Task.FromResult(clientAccountService.GetCreditCard(model.CreditCardNumber)));
        }
    }
}
