CREATE TABLE [dbo].[Users] (
    [User_Id] uniqueidentifier  NOT NULL,
    [UserType_Id] uniqueidentifier  NOT NULL,
    [CreatedDatetime] datetime  NOT NULL,
    [Email] nvarchar(128)  NULL,
    [FirstName] nvarchar(64)  NOT NULL,
    [ForgotPassword] nchar(64)  NULL,
    [ForgotPasswordIndicator] bit  NOT NULL,
    [LastName] nvarchar(64)  NOT NULL,
    [Password] nvarchar(64)  NOT NULL,
    [Phone] nvarchar(30)  NULL,
    [UniqueUserKey] nvarchar(32)  NOT NULL,
    [UpdatedDatetime] datetime  NOT NULL,
    [UserName] nvarchar(64)  NOT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([User_Id])
);