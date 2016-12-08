using EternityFramework.Migrations;
using EternityFramework.Migrations.Attributes;

namespace Infrastructure.DbMigrations.Migrations
{
    [MigrationOrder(MigrationOrder = 1)]
    public class InitialEntitiesAndData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                 CREATE TABLE [dbo].[Role](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,
                    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id])
                 )

                 CREATE TABLE [dbo].[City](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,
                    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([Id])
                 )

                 CREATE TABLE [dbo].[Country](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,
                    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([Id])
                 )

                 CREATE TABLE [dbo].[MaritalStatus](
                    [Id] [bigint] IDENTITY(1, 1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,
                    CONSTRAINT [PK_MaritalStatus] PRIMARY KEY CLUSTERED ([Id])
                 )

                 CREATE TABLE [dbo].[Disability](
                    [Id] [bigint] IDENTITY(1, 1) NOT NULL,
                    [Name] [nvarchar](max) NOT NULL,
                    CONSTRAINT [PK_Disability] PRIMARY KEY CLUSTERED ([Id])
                 )");

            Sql(@"
                 CREATE TABLE [dbo].[User](
                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [FailedLoginAttemptsCount] [int] NOT NULL,
	                [Password] [nvarchar](max) NOT NULL,
	                [PasswordSalt] [nvarchar](max) NULL,
	                [RoleId] [bigint] NOT NULL,
	                [UserName] [nvarchar](max) NULL,
                    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role](Id)
                 )

                 CREATE TABLE [dbo].[Client](
	                [Id] [bigint] IDENTITY(1,1) NOT NULL,
                    [FirstName] [nvarchar](max) NOT NULL,
                    [LastName] [nvarchar](max) NOT NULL,
                    [MiddleName] [nvarchar](max) NOT NULL,
                    [BirthDate] [date] NOT NULL,
                    [PassportSeria] [nvarchar](max) NOT NULL,
                    [PassportNo] [nvarchar](max) NOT NULL,
                    [IssuedBy] [nvarchar](max) NOT NULL,
                    [IssueDate] [date] NOT NULL,
                    [IdentificationNo] [nvarchar](max) NOT NULL,
                    [PlaceOfBirth] [nvarchar](max) NOT NULL,
                    [ActualResidenceCityId] [bigint] NOT NULL,
                    [ActualResidenceAddress] [nvarchar](max) NOT NULL,
                    [HomePhone] [nvarchar](max) NULL,
                    [MobilePhone] [nvarchar](max) NULL,
	                [Email] [nvarchar](max) NULL,
                    [WorkPlace] [nvarchar](max) NULL,
                    [WorkPosition] [nvarchar](max) NULL,
                    [RegistrationCityId] [bigint] NOT NULL,
                    [MaritalStatusId] [bigint] NOT NULL,
                    [CitizenshipCountryId] [bigint] NOT NULL,
                    [DisabilityId] [bigint] NOT NULL,
                    [IsPensioner] [bit] NOT NULL,
                    [MonthlyIncome] [money] NULL,

                    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([Id]),
                    CONSTRAINT [FK_Client_ActualResidenceCity] FOREIGN KEY ([ActualResidenceCityId]) REFERENCES [dbo].[City](Id),
                    CONSTRAINT [FK_Client_RegistrationCity] FOREIGN KEY ([RegistrationCityId]) REFERENCES [dbo].[City](Id),
                    CONSTRAINT [FK_Client_MaritalStatus] FOREIGN KEY ([MaritalStatusId]) REFERENCES [dbo].[MaritalStatus](Id),
                    CONSTRAINT [FK_Client_CitizenshipCountry] FOREIGN KEY ([CitizenshipCountryId]) REFERENCES [dbo].[Country](Id),
                    CONSTRAINT [FK_Client_Disability] FOREIGN KEY ([DisabilityId]) REFERENCES [dbo].[Disability](Id)
                 )");

            Sql(@"    
                SET IDENTITY_INSERT [dbo].[Role] ON
                INSERT INTO [dbo].[Role] (Id, Name) VALUES (1, N'User')
                INSERT INTO [dbo].[Role] (Id, Name) VALUES (2, N'Admin')
                SET IDENTITY_INSERT [dbo].[Role] OFF

                SET IDENTITY_INSERT [dbo].[City] ON
                INSERT INTO [dbo].[City] (Id, Name) VALUES (1, N'Минск')
                INSERT INTO [dbo].[City] (Id, Name) VALUES (2, N'Москва')
                INSERT INTO [dbo].[City] (Id, Name) VALUES (3, N'Вильнюс')
                INSERT INTO [dbo].[City] (Id, Name) VALUES (4, N'Варшава')
                INSERT INTO [dbo].[City] (Id, Name) VALUES (5, N'Рига')
                SET IDENTITY_INSERT [dbo].[City] OFF

                SET IDENTITY_INSERT [dbo].[Country] ON
                INSERT INTO [dbo].[Country] (Id, Name) VALUES (1, N'Беларусь')
                INSERT INTO [dbo].[Country] (Id, Name) VALUES (2, N'Россия')
                INSERT INTO [dbo].[Country] (Id, Name) VALUES (3, N'Литва')
                INSERT INTO [dbo].[Country] (Id, Name) VALUES (4, N'Латвия')
                INSERT INTO [dbo].[Country] (Id, Name) VALUES (5, N'Польша')
                SET IDENTITY_INSERT [dbo].[Country] OFF

                SET IDENTITY_INSERT [dbo].[MaritalStatus] ON
                INSERT INTO [dbo].[MaritalStatus] (Id, Name) VALUES (1, N'Женат(а) или Замужем)')
                INSERT INTO [dbo].[MaritalStatus] (Id, Name) VALUES (2, N'Разведен(а)')
                INSERT INTO [dbo].[MaritalStatus] (Id, Name) VALUES (3, N'Холост')
                SET IDENTITY_INSERT [dbo].[MaritalStatus] OFF

                SET IDENTITY_INSERT [dbo].[Disability] ON
                INSERT INTO [dbo].[Disability] (Id, Name) VALUES (1, N'Аутизм')
                INSERT INTO [dbo].[Disability] (Id, Name) VALUES (2, N'Хроническое заболевание')
                INSERT INTO [dbo].[Disability] (Id, Name) VALUES (3, N'Потеря слуха и глухота')
                INSERT INTO [dbo].[Disability] (Id, Name) VALUES (4, N'Интеллектуальная недееспособность')
                INSERT INTO [dbo].[Disability] (Id, Name) VALUES (5, N'Неспособность к обучению')
                INSERT INTO [dbo].[Disability] (Id, Name) VALUES (6, N'Потеря памяти')
                INSERT INTO [dbo].[Disability] (Id, Name) VALUES (7, N'Потеря зрения и слепота')
                SET IDENTITY_INSERT [dbo].[Disability] OFF");
        }

        public override void Down()
        {
            Sql(@"DROP TABLE [dbo].[Client]");
            Sql(@"DROP TABLE [dbo].[User]");
            Sql(@"DROP TABLE [dbo].[Disability]");
            Sql(@"DROP TABLE [dbo].[MaritalStatus]");
            Sql(@"DROP TABLE [dbo].[Country]");
            Sql(@"DROP TABLE [dbo].[City]");
            Sql(@"DROP TABLE [dbo].[Role]");
        }
    }
}
