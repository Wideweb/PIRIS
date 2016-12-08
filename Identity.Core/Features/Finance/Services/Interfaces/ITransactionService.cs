using Identity.Core.Features.Finance.Models;
using System.Threading.Tasks;

namespace Identity.Core.Features.Finance.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<long> AddClientTransaction(long accountId, decimal amount, long? transactionTypeId = null, string description = null);
        Task<long> AddTransaction(long accountId, decimal amount, long? transactionTypeId = null, string description = null);
    }
}
