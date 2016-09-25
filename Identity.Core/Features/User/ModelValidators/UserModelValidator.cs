using FluentValidation;
using Identity.Core.Features.User.Models;

namespace Identity.Core.Features.User.ModelValidators
{
    using Resources = Resources.Resources;

    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(it => it.UserName).NotEmpty().WithMessage(Resources.User_Name_Must_Be_Specified);
            RuleFor(it => it.Password).NotEmpty().WithMessage(Resources.Password_Must_Be_Specified);
        }
    }
}
