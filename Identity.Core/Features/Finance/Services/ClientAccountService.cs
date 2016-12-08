using System.Threading.Tasks;
using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Domain;
using Infrastructure.Models.Finance;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System;
using Common.Core.Exceptions;

namespace Identity.Core.Features.Finance.Services
{
    public class ClientAccountService : IClientAccountService
    {
        private readonly IdentityContext context;
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;

        public ClientAccountService(
            IdentityContext context, 
            IMapper mapper,
            ITransactionService transactionService)
        {
            this.context = context;
            this.mapper = mapper;
            this.transactionService = transactionService;
        }

        public async Task<long> SaveClientAccount(ClientAccountModel clientAccount)
        {
            if (clientAccount.IsNew)
            {
                //using (var transaction = context.DbQuery.BeginTransaction())
                //{
                //    try
                //    {
                        var accountId = await context.Accounts.Add(mapper.Map<Account>(clientAccount));

                        var clientAccountId = await context.ClientAccounts.Add(new ClientAccount
                        {
                            AccountId = accountId,
                            ClientId = clientAccount.ClientId
                        });

                        if (clientAccount.Amount != 0)
                        {
                            await transactionService.AddTransaction(accountId, clientAccount.Amount);
                        }
                        //transaction.Commit();
                        return clientAccountId;
                    //}
                    //catch (Exception ex)
                    //{
                    //    transaction.Rollback();
                    //    throw ex;
                    //}
                //}
            }

            await context.Accounts.Update(mapper.Map<Account>(clientAccount));
            return clientAccount.Id;
        }

        public async Task<ClientAccountModel> GetClientAccount(long clientAccountId)
        {
            var clientAccount = context.ClientAccounts.Where(it => it.Id == clientAccountId).FirstOrDefault();
            var account = context.Accounts.Where(it => it.Id == clientAccount.AccountId).FirstOrDefault();

            var model = mapper.Map<ClientAccountModel>(account);
            model.Id = clientAccount.Id;
            model.ClientId = clientAccount.ClientId;

            return model;
        }

        public long GetAccountId(long clientAccountId)
        {
            return context.ClientAccounts
                .Where(it => it.Id == clientAccountId)
                .Select(it => it.AccountId)
                .FirstOrDefault();
        }

        public Task<DepositModel> GetClientAccountDeposit(long clientAccountId)
        {
            var deposit = context.Deposits.Where(it => it.ClientAccountId == clientAccountId).FirstOrDefault();
            return Task.FromResult(mapper.Map<DepositModel>(deposit));
        }

        public List<ClientAccountListModel> GetClientAccountList()
        {
            return context.DbQuery.Get<List<ClientAccountListModel>>(@"
                SELECT 
                    ac.Id, 
					c.LastName + ' ' + c.FirstName + ' ' + c.MiddleName AS Client,
                    ap.Name + '('+ aat.Name +')' AS AccountPlanName, 
                    ac.Amount, 
                    CAST(ap.Number AS VARCHAR) + '-' + CAST(ac.IndividualNumber AS VARCHAR) AS Number,
                    ca.AccountId
                FROM [dbo].[Account] AS ac 
                JOIN [dbo].[AccountPlan] AS ap
	                ON ac.AccountPlanId = ap.Id
                JOIN [dbo].[AccountActivityType] AS aat
	                ON ap.AccountActivityTypeId = aat.Id
				JOIN [dbo].[ClientAccount] AS ca
	                ON ca.AccountId = ac.Id
				JOIN [dbo].[Client] AS c
	                ON c.Id = ca.ClientId
                WHERE ac.AccountTypeId = 1");
        }

        public string GetAccountBalanceByCreditCardNumber(string creditCardNumber)
        {
            var creditCard = GetCreditCard(creditCardNumber);

            var amount = context.DbQuery.Get<decimal?>($@"
                SELECT account.Amount 
                FROM [CreditCard] AS creditCard
                JOIN [ClientAccount] AS clientAccount 
	                ON clientAccount.Id = creditCard.ClientAccountId
                JOIN [Account] AS account 
	                ON account.Id = clientAccount.AccountId
                WHERE creditCard.CardNumber = N'{creditCardNumber}'");

            if (amount == null)
            {
                throw new PirisObjectNotFoundException(nameof(CreditCard));
            }

            return $"{amount.Value} {GetClientAccountCurrencyName(creditCard.ClientAccountId)}";
        }

        public async Task<string> WithdrawCash(ATMWithdrawCashModel model)
        {
            using (var transaction = context.DbQuery.BeginTransaction())
            {
                try
                {
                    var creditCard = GetCreditCard(model.CreditCardNumber);

                    await transactionService.AddClientTransaction(
                        creditCard.ClientAccountId,
                        model.Amount.Value * -1,
                        description: "ATM Withdraw Cash");

                    transaction.Commit();

                    return $"{model.Amount} {GetClientAccountCurrencyName(creditCard.ClientAccountId)}";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private string GetClientAccountCurrencyName(long clientAccountId)
        {
            return context.DbQuery.Get<string>($@"
                        SELECT currency.Name
                        FROM [ClientAccount] AS clientAccount 
                        JOIN [Account] AS account 
	                        ON account.Id = clientAccount.AccountId
                        JOIN [Currency] AS currency 
	                        ON currency.Id = account.CurrencyId
                        WHERE clientAccount.Id = {clientAccountId}");
        }

        public CreditCardModel GetCreditCard(string creditCardNumber)
        {
            var creditCard = context.CreditCards
                .Where(it => it.CardNumber == creditCardNumber)
                .FirstOrDefault();

            if (creditCard == null)
            {
                throw new PirisObjectNotFoundException(nameof(CreditCard));
            }

            return mapper.Map<CreditCardModel>(creditCard);
        }
    }
}
