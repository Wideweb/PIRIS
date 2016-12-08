using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 7)]
    public class AddedCreditAndCreditPlan : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE TABLE [dbo].[CreditPlan](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [DurationInMonths] [int] NULL,
                    [AmountFrom] decimal(19,4) NULL,
                    [AmountUpTo] decimal(19,4) NOT NULL,
                    [Rate] decimal(19,10) NOT NULL,
                    [Description] nvarchar(max) NULL,

                    CONSTRAINT [PK_CreditPlan] PRIMARY KEY CLUSTERED ([Id])
                )

                CREATE TABLE [dbo].[CreditPlanCurrency](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [CurrencyId] [bigint] NOT NULL,
                    [CreditPlanId] [bigint] NOT NULL,

                    CONSTRAINT [PK_CreditPlanCurrency] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_CreditPlanCurrency_CreditPlan] FOREIGN KEY ([CreditPlanId]) REFERENCES [dbo].[CreditPlan](Id),
                    CONSTRAINT [FK_CreditPlanCurrency_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency](Id)
                )

                CREATE TABLE [dbo].[Credit](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [CreditPlanId] [bigint] NOT NULL,
                    [ClientAccountId] [bigint] NOT NULL,
                    [Amount] decimal(19,4) NOT NULL,
                    [StartDate] [date] NOT NULL,

                    CONSTRAINT [PK_Credit] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_Credit_CreditPlan] FOREIGN KEY ([CreditPlanId]) REFERENCES [dbo].[CreditPlan](Id),
                    CONSTRAINT [FK_Credit_ClientAccount] FOREIGN KEY ([ClientAccountId]) REFERENCES [dbo].[ClientAccount](Id)
                )
            ");

            Sql(@"   
                SET IDENTITY_INSERT [dbo].[CreditPlan] ON
                INSERT INTO [dbo].[CreditPlan] (Id, AmountFrom, AmountUpTo, Rate, DurationInMonths, Description) 
                    VALUES (1, NULL, 20000, 29.9, 48, N'На рефинансирование');
                INSERT INTO [dbo].[CreditPlan] (Id, AmountFrom, AmountUpTo, Rate, DurationInMonths, Description) 
                    VALUES (2, NULL, 1000, 32.2, 24, N'Легкий');
                INSERT INTO [dbo].[CreditPlan] (Id, AmountFrom, AmountUpTo, Rate, DurationInMonths, Description) 
                    VALUES (3, NULL, 10000, 32.2, 36, N'Мечтай');
                INSERT INTO [dbo].[CreditPlan] (Id, AmountFrom, AmountUpTo, Rate, DurationInMonths, Description) 
                    VALUES (4, 100, 10000, 32.2, 36, N'Ленейный почтовой');
                INSERT INTO [dbo].[CreditPlan] (Id, AmountFrom, AmountUpTo, Rate, DurationInMonths, Description) 
                    VALUES (5, 10000, 20000, 32.2, 36, N'Большой');
                INSERT INTO [dbo].[CreditPlan] (Id, AmountFrom, AmountUpTo, Rate, DurationInMonths, Description) 
                    VALUES (6, 50, 500, 32.2, 1, N'До зарплаты');
                INSERT INTO [dbo].[CreditPlan] (Id, AmountFrom, AmountUpTo, Rate, DurationInMonths, Description) 
                    VALUES (7, 100, 10000, 55, 36, N'Простой');
                SET IDENTITY_INSERT [dbo].[CreditPlan] OFF
            ");

            Sql(@"   
                INSERT INTO [dbo].[CreditPlanCurrency] (CreditPlanId, CurrencyId) VALUES (1, 3);
                INSERT INTO [dbo].[CreditPlanCurrency] (CreditPlanId, CurrencyId) VALUES (2, 3);
                INSERT INTO [dbo].[CreditPlanCurrency] (CreditPlanId, CurrencyId) VALUES (3, 3);
                INSERT INTO [dbo].[CreditPlanCurrency] (CreditPlanId, CurrencyId) VALUES (4, 3);
                INSERT INTO [dbo].[CreditPlanCurrency] (CreditPlanId, CurrencyId) VALUES (5, 3);
                INSERT INTO [dbo].[CreditPlanCurrency] (CreditPlanId, CurrencyId) VALUES (6, 3);
                INSERT INTO [dbo].[CreditPlanCurrency] (CreditPlanId, CurrencyId) VALUES (7, 3);

                SET IDENTITY_INSERT [dbo].[AccountPlan] ON
                INSERT INTO [dbo].[AccountPlan] (Id, Name, Number, AccountActivityTypeId) VALUES (7, N'Краткосрочные кредиты физическим лицам на потребительские нужды', 2412, 1);
                INSERT INTO [dbo].[AccountPlan] (Id, Name, Number, AccountActivityTypeId) VALUES (8, N'Долгосрочные кредиты физическим лицам на потребительские нужды', 2427, 1)
                SET IDENTITY_INSERT [dbo].[AccountPlan] OFF
            ");
        }

        public override void Down()
        {
        }
    }
}
