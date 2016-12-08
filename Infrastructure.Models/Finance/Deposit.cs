using System;

namespace Infrastructure.Models.Finance
{
    public class Deposit : Entity
    {
        public long ClientAccountId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Amount { get; set; }
        public long DepositPlanId { get; set; }
    }
}
