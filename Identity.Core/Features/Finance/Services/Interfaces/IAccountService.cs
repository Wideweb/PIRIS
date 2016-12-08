using Identity.Core.Features.Finance.Models;
using System.Linq;

namespace Identity.Core.Features.Finance.Services.Interfaces
{
    public interface IAccountService
    {
        long GetAccountCurrencyId(long accountId);
        AccountModel GetAccount(long accountId);
        IQueryable<AccountTransactionListModel> GetAccountTransactionList(long accountId);
    }
}
