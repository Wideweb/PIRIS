using Identity.Core.Features.User.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Features.User.Services.Interfaces
{
    public interface IClientService
    {
        Task<long> SaveClient(ClientModel clientModel);

        Task<ClientModel> GetClientById(long id);

        Task<IQueryable<ClientListModel>> GetClientList();
    }
}
