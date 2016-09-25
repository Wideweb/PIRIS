using Common.Core.Cqrs;
using Infrastructure.Models.Identity;

namespace Identity.Core.Features.User.Commands
{
    public class SaveUserCommand : ICommand<long>
    {
        public ApplicationUser User { get; set; }
    }
}
