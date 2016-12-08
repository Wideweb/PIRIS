using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 11)]
    public class AddedCurrencyRate : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE TABLE [dbo].[CurrencyRate](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Rate] decimal(19,10) NOT NULL,
                    [FromCurrencyId] [bigint] NOT NULL,
                    [ToCurrencyId] [bigint] NOT NULL,

                    CONSTRAINT [PK_CurrencyRate] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_CurrencyRate_FromCurrency] FOREIGN KEY ([FromCurrencyId]) REFERENCES [dbo].[Currency](Id),
                    CONSTRAINT [FK_CurrencyRate_ToCurrency] FOREIGN KEY ([ToCurrencyId]) REFERENCES [dbo].[Currency](Id)
                )

                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (1, 3, 1.9600);
                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (3, 1, 0.5076);

                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (2, 3, 2.0780);
                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (3, 2, 0.4812);

                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (2, 1, 1.06);
                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (1, 2, 0.94);

                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (1, 1, 1);
                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (2, 2, 1);
                INSERT INTO [dbo].[CurrencyRate] (FromCurrencyId, ToCurrencyId, Rate) VALUES (3, 3, 1);
            ");
        }

        public override void Down()
        {
        }
    }
}
