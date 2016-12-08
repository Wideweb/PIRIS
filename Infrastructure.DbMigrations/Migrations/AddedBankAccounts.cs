using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 4)]
    public class AddedBankAccounts : DbMigration
    {
        public override void Up()
        {
            Sql(@"   
                INSERT INTO [dbo].[Account] (AccountTypeId, AccountPlanId, IndividualNumber, Amount) VALUES (2, 1, 10000001, 1000000)
                INSERT INTO [dbo].[Account] (AccountTypeId, AccountPlanId, IndividualNumber, Amount) VALUES (2, 2, 10000002, 1000000)
            ");
        }

        public override void Down()
        {
        }
    }
}
