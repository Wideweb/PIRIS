using Identity.Core.Features.User.Models;
using System.Threading.Tasks;

namespace Identity.Core.Features.User.Services.Interfaces
{
    public interface IUserService
    {
        Task<long> SaveUser(UserModel userModel);

        Task<UserModel> GetUserById(long id);
    }
}
