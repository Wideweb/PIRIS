using Common.Core.Cqrs;
using Common.Core.DataAccess;
using Identity.Core.Features.User.Commands;
using Infrastructure.Models.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Features.User.CommandHandlers
{
    public class SaveUserCommandHandler : ICommandHandler<SaveUserCommand, long>
    {
        private readonly IDbQuery dbQuery;

        public SaveUserCommandHandler(IDbQuery dbQuery)
        {
            this.dbQuery = dbQuery;
        }

        public Task<long> HandleAsync(SaveUserCommand command)
        {
            if (command.User.IsNew)
            {
                return Create(command.User);
            }else
            {
                return Update(command.User);
            }
        }

        private async Task<long> Update(ApplicationUser user)
        {
            await dbQuery.ExecuteCommandAsync(
                $@"UPDATE [dbo].[{user.GetDbTableName()}] SET
                    {user.GetUpdatePropertiesString()}
                   WHERE Id = @Id
                ", user.GetSqlParameters(excludeId: false).ToArray());

            return user.Id;
        }

        private Task<long> Create(ApplicationUser user)
        {
            return dbQuery.ExecuteInsertCommandAsync(
                $@"INSERT INTO [dbo].[{user.GetDbTableName()}]
                (
                    {user.GetPropertiesString()}
                ) 
                VALUES
                (
                    {user.GetSqlParametersString()}
                )", user.GetSqlParameters().ToArray());
        }
    }
}
