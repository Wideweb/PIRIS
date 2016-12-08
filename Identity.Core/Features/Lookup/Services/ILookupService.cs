using Identity.Core.Features.Lookup.Models;
using System.Linq;

namespace Identity.Core.Features.Lookup.Services
{
    public interface ILookupService
    {
        IQueryable<LookupModel> GetCountryLookups();

        IQueryable<LookupModel> GetCityLookups();

        IQueryable<LookupModel> GetDisabilityLookups();

        IQueryable<LookupModel> GetMaritalStatusLookups();

        IQueryable<LookupModel> GetDepositTypeLookups();

        IQueryable<LookupModel> GetCurrencyLookups();

        IQueryable<LookupModel> GetClientLookups();

        IQueryable<LookupModel> GetAccountPlanLookups();

        IQueryable<LookupModel> GetDepositPlanLookups(long? depositTypeId, long? currencyId);

        IQueryable<LookupModel> GetClientAccountLookups(long? clientId, long? accountPlanId);

        IQueryable<LookupModel> GetCreditPlanLookups(long? creditTypeId, long? currencyId);

        IQueryable<LookupModel> GetCreditTypeLookups();
    }
}
