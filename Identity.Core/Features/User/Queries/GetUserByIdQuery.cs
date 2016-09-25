using Common.Core.Cqrs;
using Identity.Core.Features.User.Models;

namespace Identity.Core.Features.User.Queries
{
    public class GetUserByIdQuery : IQuery<UserModel>
    {
        public long Id { get; set; }
    }
}
