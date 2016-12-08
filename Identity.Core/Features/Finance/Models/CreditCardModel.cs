using Common.Core.Models;
using System;

namespace Identity.Core.Features.Finance.Models
{
    public class CreditCardModel : BaseModel
    {
        public string CardNumber { get; set; }
        public long CreditCardTypeId { get; set; }
        public byte ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public long ClientAccountId { get; set; }
    }
}
