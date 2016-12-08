using System.Threading.Tasks;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Features.Finance.Services.Interfaces;
using Identity.Core.Domain;
using AutoMapper;
using Infrastructure.Models.Finance;
using System.Linq;
using System;
using Common.Core.Exceptions;

namespace Identity.Core.Features.Finance.Services
{
    public class DepositService : IDepositService
    {
        private readonly IdentityContext context;
        private readonly IMapper mapper;
        private readonly IClientAccountService clientAccountService;
        private readonly ITransactionService transactionService;
        private readonly IAccountService accountService;

        public DepositService(
            IdentityContext context,
            IMapper mapper,
            IClientAccountService clientAccountService,
            ITransactionService transactionService,
            IAccountService accountService)
        {
            this.context = context;
            this.mapper = mapper;
            this.clientAccountService = clientAccountService;
            this.transactionService = transactionService;
            this.accountService = accountService;
        }

        public async Task<long> SaveDeposit(CreateDepositModel depositModel)
        {
            if (depositModel.IsNew)
            {
                using (var transaction = context.DbQuery.BeginTransaction())
                {
                    try
                    {
                        var deposit = mapper.Map<Deposit>(depositModel);
                        var masterAccountId = clientAccountService.GetAccountId(depositModel.MasterClientAccountId.Value);

                        await transactionService.AddTransaction(masterAccountId, depositModel.Amount.Value * -1, description: "Crete Deposit");

                        var clientAccountId = await clientAccountService.SaveClientAccount(new ClientAccountModel
                        {
                            AccountPlanId = depositModel.DepositTypeId.Value == 1 ? 5 : 6,
                            Amount = 0,
                            ClientId = depositModel.ClientId.Value,
                            MasterAccountId = masterAccountId,
                            CurrencyId = accountService.GetAccountCurrencyId(masterAccountId)
                        });

                        await transactionService.AddClientTransaction(clientAccountId, depositModel.Amount.Value, description: "Create Deposit");

                        deposit.ClientAccountId = clientAccountId;
                        var depositId = await context.Deposits.Add(deposit);

                        transaction.Commit();
                        return depositId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }

            await context.Deposits.Update(mapper.Map<Deposit>(depositModel));
            return depositModel.Id;
        }

        public Task<DepositModel> GetDeposit(long depositId)
        {
            var deposit = context.DbQuery.Get<DepositModel>(
                $@"SELECT 
                    dp.DepositTypeId, 
                    d.Id, 
                    d.DepositPlanId, 
                    d.Amount,
                    d.StartDate,
                    d.ClientAccountId,
                    mca.Id AS MasterClientAccountId, 
                    ca.ClientId
                FROM Deposit as d
                JOIN ClientAccount as ca
	                ON d.ClientAccountId = ca.Id
                JOIN Account as a
	                ON a.Id = ca.AccountId
                JOIN ClientAccount as mca
	                ON a.MasterAccountId = mca.AccountId
                JOIN DepositPlan as dp
	                ON dp.Id = d.DepositPlanId
                WHERE d.Id = {depositId}");

            if(deposit == null)
            {
                throw new PirisObjectNotFoundException(nameof(Deposit));
            }

            return Task.FromResult(deposit);
        }

        public IQueryable<DepositPlanListModel> GetDepositPlanList(long depositTypeId, long currencyId)
        {
            var a = context.DepositPlans
                .Where(it => it.DepositTypeId == depositTypeId && it.CurrencyId == currencyId)
                .Select(it => new DepositPlanListModel
                {
                    Id = it.Id,
                    DurationInMonths = it.DurationInMonths,
                    Rate = it.Rate 
                }).ToList();

            return a.AsQueryable();
        }
    }
}
