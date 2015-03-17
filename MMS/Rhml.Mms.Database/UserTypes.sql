CREATE TABLE [dbo].[UserTypes] (
    [UserType_Id] uniqueidentifier  NOT NULL,
    [Type] nvarchar(64)  NOT NULL, 
    CONSTRAINT [PK_UserTypes] PRIMARY KEY ([Type])
);