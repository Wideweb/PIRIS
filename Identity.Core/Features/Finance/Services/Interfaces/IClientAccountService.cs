using Identity.Core.Features.Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Core.Features.Finance.Services.Interfaces
{
    public interface IClientAccountService
    {
        Task<long> SaveClientAccount(ClientAccountModel clientAccount);
        Task<ClientAccountModel> GetClientAccount(long clientAccountId);
        Task<DepositModel> GetClientAccountDeposit(long clientAccountId);
        List<ClientAccountListModel> GetClientAccountList();
        long GetAccountId(long clientAccountId);
        string GetAccountBalanceByCreditCardNumber(string creditCardNumber);
        Task<string> WithdrawCash(ATMWithdrawCashModel model);
        CreditCardModel GetCreditCard(string creditCardNumber);
    }
}
