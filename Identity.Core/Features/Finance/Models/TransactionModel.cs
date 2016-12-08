namespace Identity.Core.Features.Finance.Models
{
    public class TransactionModel
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public long AccountId { get; set; }
    }
}
