using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 5)]
    public class AddedIndividualDepositAccountPlans : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                SET IDENTITY_INSERT [dbo].[AccountPlan] ON
                INSERT INTO [dbo].[AccountPlan] (Id, Name, Number, AccountActivityTypeId) VALUES (5, N'Вклады (депозиты) до востребования физических лиц', 3404, 2)
                INSERT INTO [dbo].[AccountPlan] (Id, Name, Number, AccountActivityTypeId) VALUES (6, N'Срочные вклады (депозиты) физических лиц', 3414, 2)
                SET IDENTITY_INSERT [dbo].[AccountPlan] OFF
            ");
        }

        public override void Down()
        {
        }
    }
}
