using System.Threading.Tasks;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Domain;
using AutoMapper;
using Infrastructure.Models.Finance;
using System;
using Common.Core.Exceptions;
using System.Linq;

namespace Identity.Core.Features.Finance.Services
{
    public class CreditService : ICreditService
    {
        private readonly IdentityContext context;
        private readonly IMapper mapper;
        private readonly IClientAccountService clientAccountService;
        private readonly ITransactionService transactionService;
        private readonly IAccountService accountService;
        private readonly IBankService bankService;

        public CreditService(
            IdentityContext context,
            IMapper mapper,
            IClientAccountService clientAccountService,
            ITransactionService transactionService,
            IAccountService accountService,
            IBankService bankService)
        {
            this.context = context;
            this.mapper = mapper;
            this.clientAccountService = clientAccountService;
            this.transactionService = transactionService;
            this.accountService = accountService;
            this.bankService = bankService;
        }

        public async Task<long> SaveCredit(CreateCreditModel creditModel)
        {
            if (creditModel.IsNew)
            {
                using (var transaction = context.DbQuery.BeginTransaction())
                {
                    try
                    {
                        var credit = mapper.Map<Credit>(creditModel);
                        var masterAccountId = clientAccountService.GetAccountId(creditModel.MasterClientAccountId.Value);

                        await transactionService.AddTransaction(masterAccountId, creditModel.Amount.Value, description: "Create Credit");

                        var clientAccountId = await clientAccountService.SaveClientAccount(new ClientAccountModel
                        {
                            AccountPlanId = 7, //Краткосрочные кредиты физическим лицам на потребительские нужды
                            Amount = 0,
                            ClientId = creditModel.ClientId.Value,
                            MasterAccountId = masterAccountId,
                            CurrencyId = accountService.GetAccountCurrencyId(masterAccountId)
                        });

                        var bankAccount = await bankService.GetBankDevelopmentFundAccount();
                        await transactionService.AddTransaction(bankAccount.Id, creditModel.Amount.Value * -1, description: "Create Credit");

                        credit.ClientAccountId = clientAccountId;
                        var creditId = await context.Credits.Add(credit);

                        transaction.Commit();
                        return creditId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }

            await context.Credits.Update(mapper.Map<Credit>(creditModel));
            return creditModel.Id;
        }

        public Task<CreditModel> GetCredit(long creditId)
        {
            var credit = context.DbQuery.Get<CreditModel>(
                $@"SELECT 
	                 clientAccount.ClientId,
	                 clientAccount.Id AS ClientAccountId,
	                 masterClientAccount.Id AS MasterClientAccountId,
	                 credit.StartDate,
	                 credit.Amount,
	                 credit.CreditPlanId
                FROM [dbo].[Credit] AS credit
                JOIN [dbo].[CreditPlan] AS creditPlan
	                ON credit.CreditPlanId = creditPlan.Id
                JOIN [dbo].[ClientAccount] AS clientAccount
	                ON clientAccount.Id = credit.ClientAccountId
                JOIN [dbo].[Account] AS account
	                ON account.Id = clientAccount.AccountId
                JOIN [dbo].[ClientAccount] as masterClientAccount
	                ON masterClientAccount.AccountId = account.MasterAccountId
                WHERE credit.Id = {creditId}");

            if(credit == null)
            {
                throw new PirisObjectNotFoundException(nameof(Credit));
            }

            return Task.FromResult(credit);
        }

        public Task<CreditPlanModel> GetCreditPlan(long creditPlanId)
        {
            var creditPlan = context.CreditPlans
                .Where(it => it.Id == creditPlanId)
                .FirstOrDefault();

            if (creditPlan == null)
            {
                throw new PirisObjectNotFoundException(nameof(CreditPlan));
            }

            return Task.FromResult(mapper.Map<CreditPlanModel>(creditPlan));
        }
    }
}
