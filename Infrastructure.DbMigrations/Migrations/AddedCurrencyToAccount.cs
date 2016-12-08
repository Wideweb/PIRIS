using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 5)]
    public class AddedCurrencyToAccount : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                ALTER TABLE [dbo].[Account] ADD [CurrencyId] [bigint] NOT NULL DEFAULT 1;
                ALTER TABLE [dbo].[Account] ADD CONSTRAINT [FK_Account_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency](Id);

                UPDATE [dbo].[Account] SET [CurrencyId] = 3 WHERE AccountTypeId = 2;
            ");
        }

        public override void Down()
        {
        }
    }
}
