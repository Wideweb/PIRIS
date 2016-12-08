using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 3)]
    public class AddedDepositPlan : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE TABLE [dbo].[DepositPlan](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [DurationInMonths] [int] NOT NULL,
                    [DepositTypeId] [bigint] NOT NULL,
                    [Rate] decimal(19,10) NOT NULL,
                    [CurrencyId] [bigint] NULL,

                    CONSTRAINT [PK_DepositPlan] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_DepositPlan_DepositType] FOREIGN KEY ([DepositTypeId]) REFERENCES [dbo].[DepositType](Id),
                    CONSTRAINT [FK_DepositPlan_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency](Id)
                )

                ALTER TABLE [dbo].[Deposit] DROP CONSTRAINT [FK_Deposit_Client];
                ALTER TABLE [dbo].[Deposit] DROP CONSTRAINT [FK_Deposit_DepositType];
                ALTER TABLE [dbo].[Deposit] DROP CONSTRAINT [FK_Deposit_Currency];
                ALTER TABLE [dbo].[Deposit] DROP COLUMN [Rate], [EndDate], [CurrencyId], [DepositTypeId], [ClientId];
                ALTER TABLE [dbo].[Deposit] ADD [DepositPlanId] [bigint] NOT NULL;
                ALTER TABLE [dbo].[Deposit] ALTER COLUMN [ClientAccountId] [bigint] NOT NULL;
                ALTER TABLE [dbo].[Deposit] ADD CONSTRAINT [FK_Deposit_DepositPlan] FOREIGN KEY ([DepositPlanId]) REFERENCES [dbo].[DepositPlan](Id);
            ");

            Sql(@"   
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (1, 1, 10, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (2, 1, 10.5, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (3, 1, 11, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (6, 1, 13, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (9, 1, 13, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (12, 1, 13, 3)

                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (1, 2, 12, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (2, 2, 13, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (3, 2, 15, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (6, 2, 17, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (9, 2, 18, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (12, 2, 19, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (18, 2, 19, 3)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (24, 2, 19, 3)

                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (3, 1, 1.5, 1)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (6, 1, 2, 1)

                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (2, 2, 1, 1)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (3, 2, 1.75, 1)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (6, 2, 2.5, 1)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (12, 2, 3.25, 1)
                INSERT INTO [dbo].[DepositPlan] (DurationInMonths, DepositTypeId, Rate, CurrencyId) VALUES (24, 2, 3.5, 1)
            ");
        }

        public override void Down()
        {
        }
    }
}
