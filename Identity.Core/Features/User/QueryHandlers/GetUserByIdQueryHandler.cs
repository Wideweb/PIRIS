using AutoMapper;
using Common.Core.Cqrs;
using Common.Core.DataAccess;
using Common.Core.Exceptions;
using Identity.Core.Features.User.Models;
using Identity.Core.Features.User.Queries;
using Infrastructure.Models.Identity;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Identity.Core.Features.User.QueryHandlers
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IDbQuery dbQuery;
        private readonly IMapper mapper;

        public GetUserByIdQueryHandler(IDbQuery dbQuery, IMapper mapper)
        {
            this.dbQuery = dbQuery;
            this.mapper = mapper;
        }

        public async Task<UserModel> HandleAsync(GetUserByIdQuery query)
        {
            var user = new ApplicationUser();
            var tableName = user.GetDbTableName();
            var sqlParameter = new SqlParameter { ParameterName = "@Id", Value = query.Id };
            var result = await dbQuery.ExecuteReaderAsync(
                $@"SELECT * FROM [dbo].[{tableName}] WHERE Id = @Id", user.Load, sqlParameter);
            if (!result)
            {
                throw new PirisObjectNotFoundException(nameof(ApplicationUser));
            }
            return mapper.Map<UserModel>(user);
        }
    }
}
