using DbMigratorCore.Attributes;
using DbMigratorCore.Domain;
using System;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 1)]
    public class AddedUserEntity : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                 CREATE TABLE [dbo].[User](
	                [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [Email] [nvarchar](max) NULL,
	                [FailedLoginAttemptsCount] [int] NOT NULL,
	                [Password] [nvarchar](max) NOT NULL,
	                [PasswordSalt] [nvarchar](max) NULL,
	                [RoleId] [bigint] NOT NULL,
	                [UserName] [nvarchar](max) NULL,
                    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
                 )"
            );
        }

        public override void Down()
        {
            Sql(@"DROP TABLE [dbo].[User]");
        }
    }
}
