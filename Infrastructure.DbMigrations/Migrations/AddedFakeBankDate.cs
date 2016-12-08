using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 8)]
    public class AddedFakeBankDate : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE TABLE [dbo].[FakeBankDate](
                    [Date] [date] NOT NULL DEFAULT GETDATE()
                )
            ");

            Sql(@"
                INSERT INTO [dbo].[FakeBankDate] ([Date]) VALUES (GETDATE())
            ");
        }

        public override void Down()
        {
        }
    }
}
