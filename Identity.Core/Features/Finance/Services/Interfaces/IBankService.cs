using Identity.Core.Features.Finance.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Core.Features.Finance.Services.Interfaces
{
    public interface IBankService
    {
        Task<BankAccountModel> GetBankDevelopmentFundAccount();
        Task<BankAccountModel> GetBankInsuranceFundAccount();
        Task<BankAccountModel> GetBankAccount(long accountPlanId);
        List<BankAccountListModel> GetBankAccountList();
        Task CloseBankDay();
        Task<DateTime> GetBankDate();
        Task<List<MonthlyIncomeModel>> GetAnnualReportData();
    }
}
