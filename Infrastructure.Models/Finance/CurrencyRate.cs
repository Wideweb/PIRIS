namespace Infrastructure.Models.Finance
{
    public class CurrencyRate : Entity
    {
        public long FromCurrencyId { get; set; }
        public long ToCurrencyId { get; set; }
        public decimal Rate { get; set; }
    }
}
