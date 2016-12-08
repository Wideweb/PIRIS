using Identity.Core.Features.Finance.Models;
using System.Threading.Tasks;

namespace Identity.Core.Features.Finance.Services.Interfaces
{
    public interface ICreditService
    {
        Task<CreditModel> GetCredit(long creditId);
        Task<long> SaveCredit(CreateCreditModel creditModel);
        Task<CreditPlanModel> GetCreditPlan(long creditPlanId);
    }
}
