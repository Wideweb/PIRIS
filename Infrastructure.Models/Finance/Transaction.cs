using System;

namespace Infrastructure.Models.Finance
{
    public class Transaction : Entity
    {
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public long AccountId { get; set; }
        public long? TransactionTypeId { get; set; }
    }
}
