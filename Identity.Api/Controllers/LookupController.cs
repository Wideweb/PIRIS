using Common.Core.ActionFilters;
using Identity.Core.Features.Lookup.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [PaginationFilter]
    public class LookupController : Controller
    {
        private readonly ILookupService lookupService;

        public LookupController(ILookupService lookupService)
        {
            this.lookupService = lookupService;
        }

        [HttpGet("GetCountryLookups")]
        public IActionResult GetCountryLookups()
        {
            return Ok(lookupService.GetCountryLookups());
        }

        [HttpGet("GetCityLookups")]
        public IActionResult GetCityLookups()
        {
            return Ok(lookupService.GetCityLookups());
        }

        [HttpGet("GetDisabilityLookups")]
        public IActionResult GetDisabilityLookups()
        {
            return Ok(lookupService.GetDisabilityLookups());
        }

        [HttpGet("GetMaritalStatusLookups")]
        public IActionResult GetMaritalStatusLookups()
        {
            return Ok(lookupService.GetMaritalStatusLookups());
        }

        [HttpGet("GetDepositTypeLookups")]
        public IActionResult GetDepositTypeLookups()
        {
            return Ok(lookupService.GetDepositTypeLookups());
        }

        [HttpGet("GetCurrencyLookups")]
        public IActionResult GetCurrencyLookups()
        {
            return Ok(lookupService.GetCurrencyLookups());
        }

        [HttpGet("GetClientLookups")]
        public IActionResult GetClientLookups()
        {
            return Ok(lookupService.GetClientLookups());
        }

        [HttpGet("GetAccountPlanLookups")]
        public IActionResult GetAccountPlanLookups()
        {
            return Ok(lookupService.GetAccountPlanLookups());
        }

        [HttpGet("GetDepositPlanLookups")]
        public IActionResult GetDepositPlanLookups(long? depositTypeId, long? currencyId)
        {
            return Ok(lookupService.GetDepositPlanLookups(depositTypeId, currencyId));
        }

        [HttpGet("GetClientAccountLookups")]
        public IActionResult GetClientAccountLookups(long? clientId, long? accountPlanId)
        {
            return Ok(lookupService.GetClientAccountLookups(clientId, accountPlanId));
        }

        [HttpGet("GetCreditPlanLookups")]
        public IActionResult GetCreditPlanLookups(long? creditTypeId, long? currencyId)
        {
            return Ok(lookupService.GetCreditPlanLookups(creditTypeId, currencyId));
        }

        [HttpGet("GetCreditTypeLookups")]
        public IActionResult GetCreditTypeLookups()
        {
            return Ok(lookupService.GetCreditTypeLookups());
        }
    }
}