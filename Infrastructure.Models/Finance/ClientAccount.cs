namespace Infrastructure.Models.Finance
{
    public class ClientAccount : Entity
    {
        public long AccountId { get; set; }
        public long ClientId { get; set; }
    }
}
