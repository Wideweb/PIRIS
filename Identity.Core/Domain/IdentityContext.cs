using EternityFramework;
using EternityFramework.LinqToSql;
using Infrastructure.Models.Finance;
using Infrastructure.Models.Identity;

namespace Identity.Core.Domain
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(string connectionString) : base(connectionString) { }

        public DbQueryable<ApplicationUser> ApplicationUsers { get; set; }
        public DbQueryable<Role> Roles { get; set; }
        public DbQueryable<Client> Clients { get; set; }
        public DbQueryable<Country> Countries { get; set; }
        public DbQueryable<City> Cities { get; set; }
        public DbQueryable<Disability> Disabilities { get; set; }
        public DbQueryable<MaritalStatus> MaritalStatuses { get; set; }
        public DbQueryable<Deposit> Deposits { get; set; }
        public DbQueryable<DepositPlan> DepositPlans { get; set; }
        public DbQueryable<Account> Accounts { get; set; }
        public DbQueryable<ClientAccount> ClientAccounts { get; set; }
        public DbQueryable<Transaction> Transactions { get; set; }
        public DbQueryable<DepositType> DepositTypes { get; set; }
        public DbQueryable<Currency> Currencies { get; set; }
        public DbQueryable<CurrencyRate> CurrencyRates { get; set; }
        public DbQueryable<AccountPlan> AccountPlan { get; set; }
        public DbQueryable<Credit> Credits { get; set; }
        public DbQueryable<CreditPlan> CreditPlans { get; set; }
        public DbQueryable<CreditPlanCurrency> CreditPlanCurrencies { get; set; }
        public DbQueryable<FakeBankDate> FakeBankDate { get; set; }
        public DbQueryable<CreditType> CreditTypes { get; set; }
        public DbQueryable<CreditCard> CreditCards { get; set; }
    }
}
