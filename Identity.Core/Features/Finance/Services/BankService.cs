using AutoMapper;
using Identity.Core.Domain;
using Identity.Core.Features.Finance.Services.Interfaces;
using System;
using Identity.Core.Features.Finance.Models;
using System.Threading.Tasks;
using System.Linq;
using Identity.Core.Enumerations;
using System.Collections.Generic;
using System.Globalization;
using Infrastructure.Models.Finance;

namespace Identity.Core.Features.Finance.Services
{
    public static class DateHelper {
        public static int MonthDifference(this DateTime lValue, DateTime rValue)
        {
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
        }
    }

    public class BankService : IBankService
    {
        private readonly IdentityContext context;
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;

        public BankService(
            IdentityContext context,
            IMapper mapper,
            ITransactionService transactionService)
        {
            this.context = context;
            this.mapper = mapper;
            this.transactionService = transactionService;
        }

        public Task<DateTime> GetBankDate()
        {
            var fakeDate = context.FakeBankDate.FirstOrDefault();
            return Task.FromResult(fakeDate.Date.ToUniversalTime());
        }

        public Task AddMonthToBankDate()
        {
            var fakeDate = context.FakeBankDate.FirstOrDefault();
            fakeDate.Date = fakeDate.Date.AddMonths(1);
            return context.FakeBankDate.Update(fakeDate);
        }

        public async Task CloseBankDay()
        {
            var bankInsuranceFundAccount = await GetBankInsuranceFundAccount();
            var bankDevelopmentFundAccount = await GetBankDevelopmentFundAccount();
            var bankDate = await GetBankDate();
            var currencyRates = context.CurrencyRates.ToList();

            using (var transaction = context.DbQuery.BeginTransaction())
            {
                try
                {
                    await ProcessDeposits(bankDevelopmentFundAccount.Id, bankDate, currencyRates);
                    await ProcessCredits(bankInsuranceFundAccount.Id, bankDate, currencyRates);
                    await AddMonthToBankDate();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private async Task ProcessDeposits(long bankAccountId, DateTime bankDate, List<CurrencyRate> currencyRates)
        {
            var depositPayments = context.DbQuery.Get<List<DepositPaymentModel>>($@"
                SELECT 
	                deposit.DepositPlanId,
	                deposit.ClientAccountId,
	                deposit.Amount,
	                deposit.StartDate,
	                depositPlan.DurationInMonths,
	                depositPlan.Rate AS DepositRate,
	                DepositPlan.DepositTypeId,
	                currencyRate.Rate AS AccountToBankCurrencyRate
                FROM [dbo].[Deposit] AS deposit
                JOIN [dbo].[ClientAccount] AS clientAccount
	                ON deposit.ClientAccountId = clientAccount.Id
                JOIN [dbo].[Account] AS account
	                ON clientAccount.AccountId = account.Id
                JOIN [dbo].[CurrencyRate] currencyRate
	                ON account.CurrencyId = currencyRate.FromCurrencyId
                JOIN [dbo].[DepositPlan] depositPlan
	                ON deposit.DepositPlanId = depositPlan.Id
                WHERE currencyRate.ToCurrencyId IN 
                    (
	                    SELECT TOP 1 bank.CurrencyId 
	                    FROM [dbo].[Account] as bank 
	                    WHERE bank.AccountTypeId = 2 
		                    AND bank.AccountPlanId = 2
                    ) 
                    AND DATEADD(MONTH, depositPlan.DurationInMonths, deposit.StartDate) >= 
                        (
                            SELECT TOP 1 [Date] FROM [dbo].[FakeBankDate]
                        )
                    AND deposit.StartDate <= 
                        (
                            SELECT TOP 1 [Date] FROM [dbo].[FakeBankDate]
                        )
                ");

            foreach (var deposit in depositPayments)
            {
                var annualRate = deposit.DepositRate / 100;
                var amount = GetMonthlyAnnuityPaymentAmount(
                    annualRate, 
                    deposit.Amount * annualRate, 
                    deposit.DurationInMonths);

                await transactionService.AddClientTransaction(
                    deposit.ClientAccountId, 
                    amount, 
                    (long)TransactionTypeEnum.DepositPercent);

                await transactionService.AddTransaction(
                    bankAccountId,
                    -1 * amount * deposit.AccountToBankCurrencyRate, 
                    (long)TransactionTypeEnum.DepositPercent);
            }
        }

        private async Task ProcessCredits(long bankAccountId, DateTime bankDate, List<CurrencyRate> currencyRates)
        {
            var creditPayments = context.DbQuery.Get<List<CreditPaymentModel>>($@"
                SELECT 
	                credit.CreditPlanId,
	                credit.ClientAccountId,
	                credit.Amount,
	                credit.StartDate,
	                creditPlan.DurationInMonths,
	                creditPlan.Rate AS CreditRate,
	                creditPlan.CreditTypeId,
	                currencyRate.Rate AS AccountToBankCurrencyRate
                FROM [dbo].[Credit] AS credit
                JOIN [dbo].[ClientAccount] AS clientAccount
	                ON credit.ClientAccountId = clientAccount.Id
                JOIN [dbo].[Account] AS account
	                ON clientAccount.AccountId = account.Id
                JOIN [dbo].[CurrencyRate] currencyRate
	                ON account.CurrencyId = currencyRate.FromCurrencyId
                JOIN [dbo].[CreditPlan] creditPlan
	                ON credit.CreditPlanId = creditPlan.Id
                WHERE currencyRate.ToCurrencyId IN 
                    (
	                    SELECT TOP 1 bank.CurrencyId 
	                    FROM [dbo].[Account] as bank 
	                    WHERE bank.AccountTypeId = 2 
		                    AND bank.AccountPlanId = 2
                    )
                    AND DATEADD(MONTH, creditPlan.DurationInMonths, credit.StartDate) >= 
                        (
                            SELECT TOP 1 [Date] 
                            FROM [dbo].[FakeBankDate]
                        )
                    AND credit.StartDate <= 
                        (
                            SELECT TOP 1 [Date] FROM [dbo].[FakeBankDate]
                        )
                ");

            foreach (var credit in creditPayments)
            {
                var amount = credit.CreditTypeId == 1
                    ? GetMonthlyAnnuityPaymentAmount(
                        credit.CreditRate / 100, 
                        credit.Amount,
                        credit.DurationInMonths)
                    : GetMonthlyDifferentiatedPaymentAmount(
                        credit.CreditRate / 100, 
                        credit.Amount, 
                        credit.DurationInMonths, 
                        bankDate.MonthDifference(credit.StartDate));

                await transactionService.AddClientTransaction(
                    credit.ClientAccountId,
                    -1 * amount * credit.AccountToBankCurrencyRate, 
                    (long)TransactionTypeEnum.CreditPercent);

                await transactionService.AddTransaction(
                    bankAccountId, 
                    amount, 
                    (long)TransactionTypeEnum.CreditPercent);
            }
        }

        private decimal GetMonthlyAnnuityPaymentAmount(decimal annualRate, decimal amount, int durationInMonths)
        {
            var monthRate = ((double)annualRate / 12);
            var annuityFactor = (monthRate * Math.Pow((1 + monthRate), durationInMonths)) / (Math.Pow((1 + monthRate), durationInMonths) - 1);
            return (decimal)((double)amount * annuityFactor);
        }

        private decimal GetMonthlyDifferentiatedPaymentAmount(decimal annualRate, decimal amount, int durationInMonths, int passedMonths)
        {
            return amount / durationInMonths + (amount - amount / durationInMonths * passedMonths) * annualRate / 12;
        }

        public async Task<List<MonthlyIncomeModel>> GetAnnualReportData()
        {
            var bankInsuranceFundAccount = await GetBankInsuranceFundAccount();
            var bankDate = await GetBankDate();
            var startDate = bankDate.AddYears(-1);
            var transactions = context.Transactions
                .Where(it => it.AccountId == bankInsuranceFundAccount.Id)
                .ToList()
                .Where(it => it.Time >= startDate)
                .ToList();

            var reportData = new List<MonthlyIncomeModel>();
            for(var i = -1; i < 11; i++)
            {
                var monthIndex = (i + bankDate.Month) % 12 + 1;
                var monthTransactions = transactions
                    .Where(it => it.Time.Month == monthIndex)
                    .ToList();

                reportData.Add(new MonthlyIncomeModel
                {
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(monthIndex),
                    Credits = monthTransactions
                        .Where(t => t.TransactionTypeId == (long)TransactionTypeEnum.CreditPercent)
                        .Sum(t => t.Amount),

                    Deposits = monthTransactions
                        .Where(t => t.TransactionTypeId == (long)TransactionTypeEnum.DepositPercent)
                        .Sum(t => t.Amount)
                });
            }
            return reportData;
        }

        public Task<BankAccountModel> GetBankDevelopmentFundAccount()
        {
            return GetBankAccount((long)AccountPlanEnum.BankDevelopmentFund);
        }

        public Task<BankAccountModel> GetBankInsuranceFundAccount()
        {
            return GetBankAccount((long)AccountPlanEnum.BankInsuranceFund);
        }

        public Task<BankAccountModel> GetBankAccount(long accountPlanId)
        {
            var bankAccount = context.Accounts
                .Where(it => it.AccountTypeId == (long)AccountTypeEnum.Bank && it.AccountPlanId == accountPlanId)
                .FirstOrDefault();

            return Task.FromResult(mapper.Map<BankAccountModel>(bankAccount));
        }

        public List<BankAccountListModel> GetBankAccountList()
        {
            return context.DbQuery.Get<List<BankAccountListModel>>(@"
                SELECT 
                    ac.Id, 
                    ap.Name + '('+ aat.Name +')' AS AccountPlanName, 
                    ac.Amount, 
                    CAST(ap.Number AS VARCHAR) + '-' + CAST(ac.IndividualNumber AS VARCHAR) AS Number 
                FROM [dbo].[Account] AS ac 
                JOIN [dbo].[AccountPlan] AS ap
	                ON ac.AccountPlanId = ap.Id
                JOIN [dbo].[AccountActivityType] AS aat
	                ON ap.AccountActivityTypeId = aat.Id
                WHERE ac.AccountTypeId = 2");
        }
    }
}
