using FluentValidation;
using Identity.Core.Domain;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Resources;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace Identity.Core.Features.Finance.ModelValidators
{
    public class ATMGetAccountBalanceModelValidator : AbstractValidator<ATMGetAccountBalanceModel>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IdentityContext context;

        public ATMGetAccountBalanceModelValidator(
             IStringLocalizer<SharedResources> localizer,
             IdentityContext context)
        {
            this.localizer = localizer;
            this.context = context;
            SetValidationRules();
        }

        public void SetValidationRules()
        {
            RuleFor(it => it.CreditCardNumber)
                .NotEmpty().WithMessage(localizer["Credit_Card_Number_Must_Be_Specified"]);

            When(it => !string.IsNullOrWhiteSpace(it.CreditCardNumber), () => {
                RuleFor(it => it.CreditCardNumber).Must(DoesCreditCardExist).WithMessage(localizer["Credit Card was not found."]);
            });
        }

        private bool DoesCreditCardExist(string creditCardNumber)
        {
            var creditCard = context.CreditCards
                .Where(it => it.CardNumber == creditCardNumber)
                .FirstOrDefault();

            return creditCard != null;
        }
    }
}
