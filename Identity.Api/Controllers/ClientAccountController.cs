using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Core.ActionFilters;
using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Features.Questionnaire.Services;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientAccountController : Controller
    {
        private readonly IClientAccountService clientAccountService;

        public ClientAccountController(IClientAccountService clientAccountService)
        {
            this.clientAccountService = clientAccountService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await clientAccountService.GetClientAccount(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientAccountModel clientAccountModel)
        {
            return Ok(await clientAccountService.SaveClientAccount(clientAccountModel));
        }

        [HttpGet("GetClientAccountDeposit")]
        public async Task<IActionResult> GetClientAccountDeposit(int clientAccountId)
        {
            return Ok(await clientAccountService.GetClientAccountDeposit(clientAccountId));
        }

        [HttpGet("GetClientAccountForm")]
        public async Task<IActionResult> GetClientAccountForm()
        {
            return Ok(await Task.FromResult(new FormService().GetForm(typeof(ClientAccountModel))));
        }

        [HttpGet("GetClientAccountList")]
        public async Task<IActionResult> GetClientAccountList()
        {
            return Ok(await Task.FromResult(clientAccountService.GetClientAccountList()));
        }
    }
}
