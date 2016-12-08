using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Domain;
using AutoMapper;
using System.Linq;
using Identity.Core.Features.Finance.Models;

namespace Identity.Core.Features.Finance.Services
{
    public class AccountService : IAccountService
    {
        private readonly IdentityContext context;
        private readonly IMapper mapper;

        public AccountService(IdentityContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public long GetAccountCurrencyId(long accountId)
        {
            return context.Accounts
                .Where(it => it.Id == accountId)
                .Select(it => it.CurrencyId)
                .FirstOrDefault();
        }

        public AccountModel GetAccount(long accountId)
        {
            var account = context.Accounts
                .Where(it => it.Id == accountId)
                .FirstOrDefault();

            var model = mapper.Map<AccountModel>(account);
            return model;
        }

        public IQueryable<AccountTransactionListModel> GetAccountTransactionList(long accountId)
        {
            return context.Transactions.Where(it => it.AccountId == accountId).Select(it => new AccountTransactionListModel
            {
                Id = it.Id,
                Amount = it.Amount,
                Description = it.Description,
                Time = it.Time
            });
        }
    }
}
