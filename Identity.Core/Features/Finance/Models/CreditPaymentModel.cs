using System;

namespace Identity.Core.Features.Finance.Models
{
    public class CreditPaymentModel
    {
        public long CreditPlanId { get; set; }
        public long ClientAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInMonths { get; set; }
        public decimal CreditRate { get; set; }
        public long CreditTypeId { get; set; }
        public decimal AccountToBankCurrencyRate { get; set; }
    }
}
