using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 9)]
    public class AddedCreditType : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE TABLE [dbo].[CreditType](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,

                    CONSTRAINT [PK_CreditType] PRIMARY KEY CLUSTERED ([Id])
                )

                SET IDENTITY_INSERT [dbo].[CreditType] ON
                INSERT INTO [dbo].[CreditType] (Id, Name) VALUES (1, N'Аннуитетный')
                INSERT INTO [dbo].[CreditType] (Id, Name) VALUES (2, N'Дифференцированный')
                SET IDENTITY_INSERT [dbo].[CreditType] OFF

                ALTER TABLE [dbo].[CreditPlan] 
                ADD [CreditTypeId] [bigint] NOT NULL DEFAULT 1;

                ALTER TABLE [dbo].[CreditPlan] 
                ADD CONSTRAINT [FK_Credit_CreditType] 
                FOREIGN KEY ([CreditTypeId]) REFERENCES [dbo].[CreditType](Id);
            ");
        }

        public override void Down()
        {
        }
    }
}
