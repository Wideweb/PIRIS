using System.Linq;
using Identity.Core.Features.Lookup.Models;
using Identity.Core.Domain;
using Identity.Core.Enumerations;
using System.Collections.Generic;

namespace Identity.Core.Features.Lookup.Services
{
    public class LookupService : ILookupService
    {
        private readonly IdentityContext context;

        public LookupService(IdentityContext context)
        {
            this.context = context;
        }

        public IQueryable<LookupModel> GetCityLookups()
        {
            return context.Cities.Select(it => new LookupModel
            {
                Id = it.Id,
                Text = it.Name
            });
        }

        public IQueryable<LookupModel> GetCountryLookups()
        {
            return context.Countries.Select(it => new LookupModel
            {
                Id = it.Id,
                Text = it.Name
            });
        }

        public IQueryable<LookupModel> GetDisabilityLookups()
        {
            return context.Disabilities.Select(it => new LookupModel
            {
                Id = it.Id,
                Text = it.Name
            });
        }

        public IQueryable<LookupModel> GetMaritalStatusLookups()
        {
            return context.MaritalStatuses.Select(it => new LookupModel
            {
                Id = it.Id,
                Text = it.Name
            });
        }

        public IQueryable<LookupModel> GetDepositTypeLookups()
        {
            return context.DepositTypes.Select(it => new LookupModel
            {
                Id = it.Id,
                Text = it.Name
            });
        }

        public IQueryable<LookupModel> GetCurrencyLookups()
        {
            return context.Currencies.Select(it => new LookupModel
            {
                Id = it.Id,
                Text = it.Name
            });
        }

        public IQueryable<LookupModel> GetClientLookups()
        {
            return context.Clients
                .ToList()
                .Select(it => new LookupModel
                {
                    Id = it.Id,
                    Text = $"{it.LastName} {it.FirstName} {it.MiddleName}"
                }).AsQueryable();
        }

        public IQueryable<LookupModel> GetAccountPlanLookups()
        {
            return context.AccountPlan
                .ToList()
                .Select(it => new LookupModel
                {
                    Id = it.Id,
                    Text = it.Name + (it.AccountActivityTypeId == (long)AccountActivityTypeEnum.Active ? "(A)" : "(П)")
                }).AsQueryable();
        }

        public IQueryable<LookupModel> GetDepositPlanLookups(long? depositTypeId, long? currencyId)
        {
            return context.DepositPlans
                .Where(it => it.DepositTypeId == depositTypeId && it.CurrencyId == currencyId)
                .ToList()
                .Select(it => new LookupModel
                {
                    Id = it.Id,
                    Text = $"Rate: {it.Rate.ToString(".####")}%; {it.DurationInMonths} Months"
                }).AsQueryable();
        }

        public IQueryable<LookupModel> GetClientAccountLookups(long? clientId, long? accountPlanId)
        {
            return context.DbQuery.Get<List<LookupModel>>($@"
                SELECT 
                    ca.Id AS [Id], 
                    CAST(ap.Number AS VARCHAR) 
                    + '-' 
                    + CAST(a.IndividualNumber AS VARCHAR) 
                    + '(' 
                    + CAST(a.Amount AS VARCHAR) 
                    + ' ' 
                    + CAST(c.Name AS VARCHAR) 
                    + ')' AS [Text] 
                FROM [dbo].[ClientAccount] AS ca
                JOIN [dbo].[Account] AS a
	                ON ca.AccountId = a.Id
                JOIN [dbo].[AccountPlan] AS ap
	                ON ap.Id = a.AccountPlanId
                JOIN [dbo].[Currency] AS c
	                ON c.Id = a.CurrencyId
                WHERE ca.ClientId = {clientId}" + 
                (accountPlanId.HasValue ? $@"AND ap.Id = {accountPlanId}" : "")).AsQueryable();
        }

        public IQueryable<LookupModel> GetCreditPlanLookups(long? creditTypeId, long? currencyId)
        {
            var creditPlanCurrencies = context.CreditPlanCurrencies.ToList();
            var creditPlans = context.CreditPlans
                .Where(it => it.CreditTypeId == creditTypeId)
                .ToList();

            var request = from creditPlan in creditPlans
            join creditPlanCurrency in creditPlanCurrencies
                on creditPlan.Id equals creditPlanCurrency.CreditPlanId
            where creditPlanCurrency.CurrencyId == currencyId
                    select new LookupModel
            {
                Id = creditPlan.Id,
                Text = $@"
                    {creditPlan.Description}:
                    {(creditPlan.AmountFrom.HasValue ? $"{creditPlan.AmountFrom.Value.ToString(".00")} - " : "Up to ") + creditPlan.AmountUpTo.ToString(".00")};
                    Rate {creditPlan.Rate.ToString(".####")}%;
                    {creditPlan.DurationInMonths} Months;"
            };

            return request.AsQueryable();
        }

        public IQueryable<LookupModel> GetCreditTypeLookups()
        {
            return context.CreditTypes.Select(it => new LookupModel
            {
                Id = it.Id,
                Text = it.Name
            });
        }
    }
}
