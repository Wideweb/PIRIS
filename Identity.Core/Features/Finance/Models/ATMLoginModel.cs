using FluentValidation.Attributes;
using Identity.Core.Features.Finance.ModelValidators;

namespace Identity.Core.Features.Finance.Models
{
    [Validator(typeof(ATMLoginModelValidator))]
    public class ATMLoginModel
    {
        public string CreditCardNumber { get; set; }
    }
}
