using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Features.Finance.Models;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class CreditController : Controller
    {
        private readonly ICreditService creditServic;

        public CreditController(ICreditService creditServic)
        {
            this.creditServic = creditServic;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCreditModel creditModel)
        {
            return Ok(await creditServic.SaveCredit(creditModel));
        }
        
        public async Task<IActionResult> GetCredit(long id)
        {
            return Ok(await creditServic.GetCredit(id));
        }

        [HttpGet("GetCreditPlan")]
        public async Task<IActionResult> GetCreditPlan(long creditPlanId)
        {
            return Ok(await creditServic.GetCreditPlan(creditPlanId));
        }
    }
}
