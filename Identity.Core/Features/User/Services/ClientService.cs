using System.Threading.Tasks;
using Identity.Core.Features.User.Models;
using Identity.Core.Features.User.Services.Interfaces;
using Common.Core.Cqrs;
using AutoMapper;
using Infrastructure.Models.Identity;
using Identity.Core.Domain;
using System.Linq;
using Common.Core.Exceptions;

namespace Identity.Core.Features.User.Services
{
    public class ClientService : IClientService
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IMapper mapper;
        private readonly IdentityContext context;

        public ClientService(
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IdentityContext context)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<long> SaveClient(ClientModel clientModel)
        {
            var client = mapper.Map<Client>(clientModel);

            if (clientModel.IsNew)
            {
                return await context.Clients.Add(client);
            } else
            {
                await context.Clients.Update(client);
                return client.Id;
            }

        }

        public Task<ClientModel> GetClientById(long id)
        {
            return Task.Run(() =>
            {
                var client = context.Clients.Where(it => it.Id == id).FirstOrDefault();
                if (client == null)
                {
                    throw new PirisObjectNotFoundException(nameof(Client));
                }

                return mapper.Map<ClientModel>(client);
            });
        }

        public Task<IQueryable<ClientListModel>> GetClientList()
        {
            return Task.Run(() =>
            {
                return context.Clients.Select(it => new ClientListModel
                {
                    Id = it.Id,
                    FirstName = it.FirstName,
                    LastName = it.LastName,
                    MiddleName = it.MiddleName,
                    MobilePhone = it.MobilePhone,
                    HomePhone = it.HomePhone
                });
            });
        }
    }
}
