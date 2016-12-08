using System;

namespace Infrastructure.Models.Finance
{
    public class Credit : Entity
    {
        public long CreditPlanId { get; set; }
        public long ClientAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
    }
}
