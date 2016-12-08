using AutoMapper;
using Identity.Core.Domain;
using Identity.Core.Features.Finance.Services.Interfaces;
using System.Threading.Tasks;
using Infrastructure.Models.Finance;
using System;
using System.Linq;

namespace Identity.Core.Features.Finance.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IdentityContext context;
        private readonly IMapper mapper;

        public TransactionService(
            IdentityContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<long> AddClientTransaction(long clientAccountId, decimal amount, long? transactionTypeId = null, string description = null)
        {
            var accountId = context.ClientAccounts.Where(it => it.Id == clientAccountId).Select(it => it.AccountId).FirstOrDefault();
            return AddTransaction(accountId, amount, transactionTypeId, description);
        }

        public async Task<long> AddTransaction(long accountId, decimal amount, long? transactionTypeId = null, string description = null)
        {
            var account = context.Accounts.Where(it => it.Id == accountId).FirstOrDefault();
            account.Amount += amount;
            await context.Accounts.Update(account);

            var fakeDate = context.FakeBankDate.FirstOrDefault().Date;

            return await context.Transactions.Add(new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                Description = description,
                Time = fakeDate,
                TransactionTypeId = transactionTypeId
            });
        }
    }
}
