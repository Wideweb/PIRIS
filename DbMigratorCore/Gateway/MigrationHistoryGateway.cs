using DbMigratorCore.Attributes;
using DbMigratorCore.Utils;

namespace DbMigratorCore.Gateway
{
    [Table(Name = "MigrationHistory")]
    public class MigrationHistoryGateway : RowDataGatewayBase
    {
        public string MigrationName { get; set; }
        public long MigrationOrder { get; set; }

        public MigrationHistoryGateway(DbQuery dbQuery) : base(dbQuery) { }
    }
}
