using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 2)]
    public class AddedAccountEntites : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE TABLE [dbo].[DepositType](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,

                    CONSTRAINT [PK_DepositType] PRIMARY KEY CLUSTERED ([Id])
                )

                CREATE TABLE [dbo].[Currency](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,

                    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([Id])
                )

                CREATE TABLE [dbo].[AccountActivityType](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,

                    CONSTRAINT [PK_AccountActivityType] PRIMARY KEY CLUSTERED ([Id])
                )

                CREATE TABLE [dbo].[AccountType](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,

                    CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED ([Id])
                )

                CREATE TABLE [dbo].[AccountPlan](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,
                    [Number] [bigint] NOT NULL,
                    [AccountActivityTypeId] [bigint] NOT NULL,

                    CONSTRAINT [PK_AccountPlan] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_AccountPlan_AccountActivityType] FOREIGN KEY ([AccountActivityTypeId]) REFERENCES [dbo].[AccountActivityType](Id)
                )

                CREATE TABLE [dbo].[Account](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [AccountTypeId] [bigint] NOT NULL,
	                [AccountPlanId] [bigint] NOT NULL,
                    [MasterAccountId] [bigint] NULL,
	                [IndividualNumber] [bigint] NOT NULL,
                    [Amount] decimal(19,4) NOT NULL,

                    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_Account_AccountType] FOREIGN KEY ([AccountTypeId]) REFERENCES [dbo].[AccountType](Id),
                    CONSTRAINT [FK_Account_AccountPlan] FOREIGN KEY ([AccountPlanId]) REFERENCES [dbo].[AccountPlan](Id),
                    CONSTRAINT [FK_Account_Account] FOREIGN KEY ([MasterAccountId]) REFERENCES [dbo].[Account](Id)
                )

                CREATE TABLE [dbo].[ClientAccount](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [ClientId] [bigint] NOT NULL,
	                [AccountId] [bigint] NOT NULL,

                    CONSTRAINT [PK_ClientAccount] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_ClientAccount_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client](Id),
                    CONSTRAINT [FK_ClientAccount_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account](Id)
                )

                CREATE TABLE [dbo].[Transaction](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [Time] [datetime] NOT NULL,
                    [Description] [nvarchar](max) NULL,
	                [Amount] decimal(19,4) NOT NULL,
                    [AccountId] [bigint] NULL,

                    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_AccountTransaction_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account](Id)
                )

                CREATE TABLE [dbo].[Deposit](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [ClientId] [bigint] NULL,
                    [ClientAccountId] [bigint] NULL,
                    [DepositTypeId] [bigint] NULL,
                    [CurrencyId] [bigint] NULL,
                    [StartDate] [date] NOT NULL,
                    [EndDate] [date] NOT NULL,
                    [Amount] decimal(19,4) NOT NULL,
                    [Rate] decimal(19,10) NOT NULL,

                    CONSTRAINT [PK_Deposit] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_Deposit_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client](Id),
                    CONSTRAINT [FK_Deposit_ClientAccount] FOREIGN KEY ([ClientAccountId]) REFERENCES [dbo].[ClientAccount](Id),
                    CONSTRAINT [FK_Deposit_DepositType] FOREIGN KEY ([DepositTypeId]) REFERENCES [dbo].[DepositType](Id),
                    CONSTRAINT [FK_Deposit_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency](Id)
                )
            ");

            Sql(@"    
                SET IDENTITY_INSERT [dbo].[DepositType] ON
                INSERT INTO [dbo].[DepositType] (Id, Name) VALUES (1, N'До востребования (отзывной) с ежемесячной выплатой процентов')
                INSERT INTO [dbo].[DepositType] (Id, Name) VALUES (2, N'Срочный (безотзывной) с выплатой процентов в конце срока')
                SET IDENTITY_INSERT [dbo].[DepositType] OFF

                SET IDENTITY_INSERT [dbo].[Currency] ON
                INSERT INTO [dbo].[Currency] (Id, Name) VALUES (1, N'Доллар США')
                INSERT INTO [dbo].[Currency] (Id, Name) VALUES (2, N'Евро')
                INSERT INTO [dbo].[Currency] (Id, Name) VALUES (3, N'Белорусский рубль')
                SET IDENTITY_INSERT [dbo].[Currency] OFF

                SET IDENTITY_INSERT [dbo].[AccountActivityType] ON
                INSERT INTO [dbo].[AccountActivityType] (Id, Name) VALUES (1, N'Активный')
                INSERT INTO [dbo].[AccountActivityType] (Id, Name) VALUES (2, N'Пассивный')
                INSERT INTO [dbo].[AccountActivityType] (Id, Name) VALUES (3, N'Активно-пассивные')
                SET IDENTITY_INSERT [dbo].[AccountActivityType] OFF

                SET IDENTITY_INSERT [dbo].[AccountType] ON
                INSERT INTO [dbo].[AccountType] (Id, Name) VALUES (1, N'Клиент')
                INSERT INTO [dbo].[AccountType] (Id, Name) VALUES (2, N'Банк')
                SET IDENTITY_INSERT [dbo].[AccountType] OFF

                SET IDENTITY_INSERT [dbo].[AccountPlan] ON
                INSERT INTO [dbo].[AccountPlan] (Id, Name, Number, AccountActivityTypeId) VALUES (1, N'Денежные средства в кассе', 1010, 1)
                INSERT INTO [dbo].[AccountPlan] (Id, Name, Number, AccountActivityTypeId) VALUES (2, N'Фонд развития банка', 7327, 2)
                INSERT INTO [dbo].[AccountPlan] (Id, Name, Number, AccountActivityTypeId) VALUES (3, N'Текущие (расчетные) счета физических лиц', 3014, 2)
                INSERT INTO [dbo].[AccountPlan] (Id, Name, Number, AccountActivityTypeId) VALUES (4, N'Займы физическим лицам на потребительские цели', 2400, 2)
                SET IDENTITY_INSERT [dbo].[AccountPlan] OFF");
        }

        public override void Down()
        {
            Sql(@"DROP TABLE [dbo].[Deposit]");
            Sql(@"DROP TABLE [dbo].[Transaction]");
            Sql(@"DROP TABLE [dbo].[Account]");
            Sql(@"DROP TABLE [dbo].[ClientAccount]");
            Sql(@"DROP TABLE [dbo].[AccountPlan]");
            Sql(@"DROP TABLE [dbo].[AccountType]");
            Sql(@"DROP TABLE [dbo].[AccountActivityType]");
            Sql(@"DROP TABLE [dbo].[Currency]");
            Sql(@"DROP TABLE [dbo].[DepositType]");
        }
    }
}
