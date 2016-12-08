using Common.Core.Models;
using Identity.Core.Features.Questionnaire.Attributes;

namespace Identity.Core.Features.Finance.Models
{
    public class BankAccountModel : BaseModel
    {
        public long AccountPlanId { get; set; }
        public long? MasterAccountId { get; set; }
        public long IndividualNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
