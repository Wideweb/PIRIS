using DbMigratorCore.Utils;
using System.Data.SqlClient;

namespace DbMigratorCore.Gateway
{
    public class GatewayFinder
    {
        private DbQuery dbQuery { get; set; }

        public GatewayFinder(DbQuery dbQuery)
        {
            this.dbQuery = dbQuery;
        }

        public MigrationHistoryGateway FindLastMigration()
        {
            var tableName = RowDataGatewayHelper.GetTableName(typeof(MigrationHistoryGateway));
            var byProperty = nameof(MigrationHistoryGateway.MigrationOrder);
            var instance = new MigrationHistoryGateway(dbQuery);
            var result = dbQuery.ExecuteReader($"select * from {tableName} where {byProperty} = (select MAX({byProperty}) from {tableName})", instance.Load);
            if (!result)
            {
                return null;
            }
            return instance;
        }

        public void FindAndDeleteMigrationsAfter(long migrationOrder)
        {
            var tableName = RowDataGatewayHelper.GetTableName(typeof(MigrationHistoryGateway));
            var propertyName = nameof(MigrationHistoryGateway.MigrationOrder);
            dbQuery.ExecuteCommand($"delete from {tableName} where {propertyName} > @{propertyName}", new SqlParameter
            {
                ParameterName = $"@{propertyName}",
                Value = migrationOrder
            });
        }
    }
}
