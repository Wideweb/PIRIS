using FluentValidation.Attributes;
using Identity.Core.Features.Finance.ModelValidators;

namespace Identity.Core.Features.Finance.Models
{
    [Validator(typeof(ATMWithdrawCashModelValidator))]
    public class ATMWithdrawCashModel
    {
        public string CreditCardNumber { get; set; }
        public long? Amount { get; set; }
    }
}
