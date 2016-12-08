using Common.Core.Models;
using System;

namespace Identity.Core.Features.Finance.Models
{
    public class AccountTransactionListModel : BaseModel
    {
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
