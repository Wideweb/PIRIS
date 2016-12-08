namespace Infrastructure.Models.Finance
{
    public class DepositPlan : Entity
    {
        public decimal Rate { get; set; }
        public long CurrencyId { get; set; }
        public long DepositTypeId { get; set; }
        public int DurationInMonths { get; set; }
    }
}
