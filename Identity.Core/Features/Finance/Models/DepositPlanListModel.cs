using Common.Core.Models;
namespace Identity.Core.Features.Finance.Models
{
    public class DepositPlanListModel : BaseModel
    {
        public decimal Rate { get; set; }
        public int DurationInMonths { get; set; }
    }
}
