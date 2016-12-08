using Common.Core.Models;

namespace Identity.Core.Features.Finance.Models
{
    public class ClientAccountListModel : BaseModel
    {
        public string AccountPlanName { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public string Client { get; set; }
        public long AccountId { get; set; }
    }
}
