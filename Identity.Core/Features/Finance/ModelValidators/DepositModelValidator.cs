using FluentValidation;
using Identity.Core.Domain;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Resources;
using Microsoft.Extensions.Localization;

namespace Identity.Core.Features.Finance.ModelValidators
{
    public class DepositModelValidator : AbstractValidator<CreateDepositModel>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IdentityContext context;

        public DepositModelValidator(
             IStringLocalizer<SharedResources> localizer,
             IdentityContext context)
        {
            this.localizer = localizer;
            this.context = context;
            SetValidationRules();
        }

        public void SetValidationRules()
        {
            RuleFor(it => it.ClientId)
                .NotEmpty().WithMessage(localizer["Client_Must_Be_Specified"]);

            RuleFor(it => it.Amount)
                .NotEmpty().WithMessage(localizer["Amount_Must_Be_Specified"]);

            RuleFor(it => it.DepositPlanId)
                .NotEmpty().WithMessage(localizer["Deposit_Plan_Must_Be_Specified"]);

            RuleFor(it => it.MasterClientAccountId)
                .NotEmpty().WithMessage(localizer["Master_Client_Account_Must_Be_Specified"]);

            RuleFor(it => it.StartDate)
                .NotEmpty().WithMessage(localizer["Start_Date_Must_Be_Specified"]);
        }
    }
}
