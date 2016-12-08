using FluentValidation;
using Identity.Core.Domain;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Resources;
using Microsoft.Extensions.Localization;

namespace Identity.Core.Features.Finance.ModelValidators
{
    public class ClientAccountValidator : AbstractValidator<ClientAccountModel>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IdentityContext context;

        public ClientAccountValidator(
             IStringLocalizer<SharedResources> localizer,
             IdentityContext context)
        {
            this.localizer = localizer;
            this.context = context;
            SetValidationRules();
        }

        public void SetValidationRules()
        {
            RuleFor(it => it.AccountPlanId)
                .NotEmpty().WithMessage(localizer["Account_Plan_Must_Be_Specified"]);

            RuleFor(it => it.Amount)
                .NotEmpty().WithMessage(localizer["Amount_Must_Be_Specified"]);

            RuleFor(it => it.ClientId)
                .NotEmpty().WithMessage(localizer["Client_Must_Be_Specified"]);

            RuleFor(it => it.CurrencyId)
                .NotEmpty().WithMessage(localizer["Currency_Must_Be_Specified"]);
        }
    }
}
