using Common.Core.Models;
using FluentValidation.Attributes;
using Identity.Core.Features.User.ModelValidators;

namespace Identity.Core.Features.User.Models
{
    [Validator(typeof(UserModelValidator))]
    public class UserModel : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
