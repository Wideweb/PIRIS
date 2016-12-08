using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 12)]
    public class _12_AddedServiceAndCreditCardEntities : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE TABLE [dbo].[CreditCardType](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] nvarchar(max) NOT NULL,

                    CONSTRAINT [PK_CreditCardType] PRIMARY KEY CLUSTERED ([Id]),
                )

                CREATE TABLE [dbo].[CreditCard](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [CardNumber] nvarchar(max) NOT NULL,
                    [CreditCardTypeId] [bigint] NOT NULL,
                    [ExpiryMonth] [tinyint] NOT NULL,
                    [ExpiryYear] [smallint] NOT NULL,
                    [ClientAccountId] [bigint] NOT NULL

                    CONSTRAINT [PK_CreditCard] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_CreditCard_CreditCardType] FOREIGN KEY ([CreditCardTypeId]) REFERENCES [dbo].[CreditCardType](Id),
                    CONSTRAINT [FK_CreditCard_ClientAccount] FOREIGN KEY ([ClientAccountId]) REFERENCES [dbo].[ClientAccount](Id)
                )

                SET IDENTITY_INSERT [dbo].[CreditCardType] ON
                INSERT INTO [dbo].[CreditCardType] (Id, Name) VALUES (1, N'American Express');
                INSERT INTO [dbo].[CreditCardType] (Id, Name) VALUES (2, N'MasterCard');
                INSERT INTO [dbo].[CreditCardType] (Id, Name) VALUES (3, N'Visa');
                SET IDENTITY_INSERT [dbo].[CreditCardType] OFF
            ");
        }

        public override void Down()
        {
        }
    }
}
