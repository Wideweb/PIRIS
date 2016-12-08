using System;

namespace Infrastructure.Models.Finance
{
    public class CreditPlan : Entity
    {
        public int DurationInMonths { get; set; }
        public decimal? AmountFrom { get; set; }
        public decimal AmountUpTo { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public long CreditTypeId { get; set; }
    }
}
