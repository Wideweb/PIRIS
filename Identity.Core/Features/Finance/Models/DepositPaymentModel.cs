using System;

namespace Identity.Core.Features.Finance.Models
{
    public class DepositPaymentModel
    {
        public long DepositPlanId { get; set; }
        public long ClientAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInMonths { get; set; }
        public decimal DepositRate { get; set; }
        public long DepositTypeId { get; set; }
        public decimal AccountToBankCurrencyRate { get; set; }
    }
}
