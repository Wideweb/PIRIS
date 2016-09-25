using System;
using System.Threading.Tasks;
using Identity.Core.Features.User.Models;
using Identity.Core.Features.User.Services.Interfaces;
using Common.Core.Cqrs;
using Identity.Core.Features.User.Commands;
using Identity.Core.Features.User.Queries;
using AutoMapper;
using Infrastructure.Models.Identity;
using Identity.Core.Enumerations;
using Microsoft.Extensions.Options;
using Identity.Core.Options;

namespace Identity.Core.Features.User.Services
{
    public class UserService : IUserService
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IMapper mapper;
        private readonly IEncryptionService encryptionService;
        private readonly IdentityOptions identityOptions;

        public UserService(
            IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            IMapper mapper, 
            IEncryptionService encryptionService,
            IOptions<IdentityOptions> identityOptions)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
            this.mapper = mapper;
            this.encryptionService = encryptionService;
            this.identityOptions = identityOptions.Value;
        }

        public Task<UserModel> GetUserById(long id)
        {
            return queryDispatcher.Dispatch<GetUserByIdQuery, UserModel>(new GetUserByIdQuery
            {
                Id = id
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

            return commandDispatcher.Dispatch<SaveUserCommand, long>(new SaveUserCommand
            {
                User = user
            });
        }
    }
}
