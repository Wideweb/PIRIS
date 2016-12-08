using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 10)]
    public class AddedTransactionType : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE TABLE [dbo].[TransactionType](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,

                    CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED ([Id])
                )

                SET IDENTITY_INSERT [dbo].[TransactionType] ON
                INSERT INTO [dbo].[TransactionType] (Id, Name) VALUES (1, N'Внесение процентов по кредиту')
                INSERT INTO [dbo].[TransactionType] (Id, Name) VALUES (2, N'Начисление процентов по депозиту')
                SET IDENTITY_INSERT [dbo].[TransactionType] OFF

                ALTER TABLE [dbo].[Transaction] ADD [TransactionTypeId] [bigint] NULL;
                ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY ([TransactionTypeId]) REFERENCES [dbo].[TransactionType](Id);

                ALTER TABLE [dbo].[Transaction] ADD [CurrencyId] [bigint] NOT NULL DEFAULT 3;
                ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [FK_Transaction_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency](Id);
            ");
        }

        public override void Down()
        {
        }
    }
}
