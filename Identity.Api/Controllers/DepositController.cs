using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Features.Finance.Models;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class DepositController : Controller
    {
        private readonly IDepositService depositService;

        public DepositController(IDepositService depositService)
        {
            this.depositService = depositService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDepositModel depositModel)
        {
            return Ok(await depositService.SaveDeposit(depositModel));
        }
        
        public async Task<IActionResult> GetDeposit(long id)
        {
            return Ok(await depositService.GetDeposit(id));
        }

        [HttpGet("GetDepositPlanList")]
        public async Task<IActionResult> GetDepositPlanList(long depositTypeId, long currencyId)
        {
            return Ok(await Task.FromResult(depositService.GetDepositPlanList(depositTypeId, currencyId)));
        }
    }
}
