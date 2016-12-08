using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 6)]
    public class AddedTriggerToGenerateAccountIndividualNumber : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                UPDATE A1
                SET [IndividualNumber] = 10000000 + rowNumber
                FROM [dbo].[Account] AS A1
                JOIN (
	                SELECT Id, ROW_NUMBER() OVER (ORDER BY AccountTypeId DESC, Id) AS rowNumber FROM [dbo].[Account]
                ) AS A2
	                ON A1.Id = A2.Id

                CREATE TRIGGER [TRG_AFTER_INSERT_Account] ON [dbo].[Account] AFTER INSERT
                AS
                BEGIN
	                DECLARE 
		                @individualNumber bigint;

					SET @individualNumber = 10000000;

	                SELECT @individualNumber = MAX(IndividualNumber) 
					FROM [dbo].[Account]
					WHERE IndividualNumber > @individualNumber;

	                UPDATE [dbo].[Account] 
		                SET @individualNumber = IndividualNumber = @individualNumber + 1
	                WHERE Id IN (
		                SELECT i.Id FROM inserted as i
	                );
                END;
            ");
        }

        public override void Down()
        {
        }
    }
}
