using Common.Core.Models;

namespace Identity.Core.Features.Finance.Models
{
    public class CreditPlanModel : BaseModel
    {
        public int DurationInMonths { get; set; }
        public decimal? AmountFrom { get; set; }
        public decimal AmountUpTo { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
    }
}
