using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Features.Questionnaire.Services;
using Identity.Core.Features.Finance.Models;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class BankController : Controller
    {
        private readonly IBankService bankService;

        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        [HttpPost("CloseBankDay")]
        public async Task<IActionResult> CloseBankDay()
        {
            await bankService.CloseBankDay();
            return Ok();
        }

        [HttpGet("GetBankAccount")]
        public async Task<IActionResult> GetBankAccount(long accountPlanId)
        {
            return Ok(await bankService.GetBankAccount(accountPlanId));
        }

        [HttpGet("GetBankAccountForm")]
        public async Task<IActionResult> GetBankAccountForm()
        {
            return Ok(await Task.FromResult(new FormService().GetForm(typeof(BankAccountModel))));
        }

        [HttpGet("GetBankAccountList")]
        public async Task<IActionResult> GetBankAccountList()
        {
            return Ok(await Task.FromResult(bankService.GetBankAccountList()));
        }

        [HttpGet("GetBankDate")]
        public async Task<IActionResult> GetBankDate()
        {
            return Ok(await bankService.GetBankDate());
        }

        [HttpGet("GetAnnualReportData")]
        public async Task<IActionResult> GetAnnualReportData()
        {
            return Ok(await bankService.GetAnnualReportData());
        }
    }
}
