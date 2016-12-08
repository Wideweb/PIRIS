using Common.Core.Models;
using System;

namespace Identity.Core.Features.Finance.Models
{
    public class CreditModel : BaseModel
    {
        public long? ClientId { get; set; }
        public long? MasterClientAccountId { get; set; }
        public long? ClientAccountId { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? Amount { get; set; }
        public long? CreditPlanId { get; set; }
        public long? CreditTypeId { get; set; }
    }
}
