using FluentValidation.Attributes;
using Identity.Core.Features.Finance.ModelValidators;

namespace Identity.Core.Features.Finance.Models
{
    [Validator(typeof(ATMGetAccountBalanceModelValidator))]
    public class ATMGetAccountBalanceModel
    {
        public string CreditCardNumber { get; set; }
    }
}
