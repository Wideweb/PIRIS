using Identity.Core.Features.Finance.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Features.Finance.Services.Interfaces
{
    public interface IDepositService
    {
        Task<DepositModel> GetDeposit(long depositId);
        Task<long> SaveDeposit(CreateDepositModel depositModel);
        IQueryable<DepositPlanListModel> GetDepositPlanList(long depositTypeId, long currencyId);
    }
}
