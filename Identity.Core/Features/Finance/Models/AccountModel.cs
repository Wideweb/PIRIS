using Common.Core.Models;

namespace Identity.Core.Features.Finance.Models
{
    public class AccountModel : BaseModel
    {
        public long AccountTypeId { get; set; }
        public long AccountPlanId { get; set; }
        public long? MasterAccountId { get; set; }
        public long CurrencyId { get; set; }
        public long IndividualNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
