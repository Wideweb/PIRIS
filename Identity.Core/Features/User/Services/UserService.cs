using System.Threading.Tasks;
using Identity.Core.Features.User.Models;
using Identity.Core.Features.User.Services.Interfaces;
using Common.Core.Cqrs;
using AutoMapper;
using Infrastructure.Models.Identity;
using Microsoft.Extensions.Options;
using Identity.Core.Options;

namespace Identity.Core.Features.User.Services
{
    using Common.Core.Exceptions;
    using Domain;
    using System.Linq;
    using Role = Enumerations.Role;

    public class UserService : IUserService
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IMapper mapper;
        private readonly IEncryptionService encryptionService;
        private readonly IdentityOptions identityOptions;
        private readonly IdentityContext context;

        public UserService(
            IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            IMapper mapper, 
            IEncryptionService encryptionService,
            IOptions<IdentityOptions> identityOptions,
            IdentityContext context)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
            this.mapper = mapper;
            this.encryptionService = encryptionService;
            this.identityOptions = identityOptions.Value;
            this.context = context;
        }

        public Task<UserModel> GetUserById(long id)
        {
            return Task.Run(() =>
            {
                var user = context.ApplicationUsers.FirstOrDefault(it => it.Id == id);
                if (user == null)
                {
                    throw new PirisObjectNotFoundException(nameof(ApplicationUser));
                }

                return mapper.Map<UserModel>(user);
            });
        }

        public Task<long> SaveUser(UserModel userModel)
        {
            var saltKey = encryptionService.CreateSaltKey(identityOptions.PasswordSaltKeySize);
            var password = encryptionService.CreatePasswordHash(userModel.Password, saltKey);
            
            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                PasswordSalt = saltKey,
                Password = password,
                RoleId = (long)Role.User,
                FailedLoginAttemptsCount = 0,
                Email = userModel.Email,
            };

            /*return commandDispatcher.Dispatch<SaveUserCommand, long>(new SaveUserCommand
            {
                User = user
            });*/

            return Task.FromResult(1L);
        }
    }
}
