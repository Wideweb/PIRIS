namespace Infrastructure.Models.Finance
{
    public class CreditCard : Entity
    {
        public string CardNumber { get; set; }
        public long CreditCardTypeId { get; set; }
        public byte ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public long ClientAccountId { get; set; }
    }
}
