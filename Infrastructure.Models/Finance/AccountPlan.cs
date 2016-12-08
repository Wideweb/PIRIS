namespace Infrastructure.Models.Finance
{
    public class AccountPlan : Entity
    {
        public string Name { get; set; }
        public long Number { get; set; }
        public long AccountActivityTypeId { get; set; }
    }
}
