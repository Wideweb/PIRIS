using System;

namespace Infrastructure.Models.Finance
{
    public class CreditPlanCurrency : Entity
    {
        public long CurrencyId { get; set; }
        public long CreditPlanId { get; set; }
    }
}
