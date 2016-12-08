using Common.Core.Models;

namespace Identity.Core.Features.Finance.Models
{
    public class BankAccountListModel : BaseModel
    {
        public string AccountPlanName { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
    }
}
