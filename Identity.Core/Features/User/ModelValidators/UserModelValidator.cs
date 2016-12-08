using FluentValidation;
using Identity.Core.Features.User.Models;
using Microsoft.Extensions.Localization;

namespace Identity.Core.Features.User.ModelValidators
{   
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        private readonly IStringLocalizer<UserModelValidator> localizer;

        public UserModelValidator(IStringLocalizer<UserModelValidator> localizer)
        {
            this.localizer = localizer;
            SetValidationRules();
        }

        public void SetValidationRules()
        {
            RuleFor(it => it.UserName).NotEmpty().WithMessage(localizer["User_Name_Must_Be_Specified"]);
        }
    }
}
