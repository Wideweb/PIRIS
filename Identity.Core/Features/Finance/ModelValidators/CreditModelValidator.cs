using FluentValidation;
using Identity.Core.Domain;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Resources;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Core.Features.Finance.ModelValidators
{
    public class CreditModelValidator : AbstractValidator<CreateCreditModel>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IdentityContext context;

        public CreditModelValidator(
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

            RuleFor(it => it.CreditPlanId)
                .NotEmpty().WithMessage(localizer["Credit_Plan_Must_Be_Specified"]);

            RuleFor(it => it.MasterClientAccountId)
                .NotEmpty().WithMessage(localizer["Master_Client_Account_Must_Be_Specified"]);

            RuleFor(it => it.StartDate)
                .NotEmpty().WithMessage(localizer["Start_Date_Must_Be_Specified"]);

            RuleFor(it => it).Must(IsCreditAmountValid).WithMessage(localizer["Invalid credit amount value, please check credit plan bounds."]);
        }

        private bool IsCreditAmountValid(CreateCreditModel createCreditModel)
        {
            if (!createCreditModel.CreditPlanId.HasValue || !createCreditModel.Amount.HasValue)
            {
                return true;
            }

            var creditPlan = context.CreditPlans
                .Where(it => it.Id == createCreditModel.CreditPlanId)
                .FirstOrDefault();

            var amount = createCreditModel.Amount.Value;

            return (!creditPlan.AmountFrom.HasValue || creditPlan.AmountFrom <= amount) && creditPlan.AmountUpTo >= amount;
        }
    }
}
