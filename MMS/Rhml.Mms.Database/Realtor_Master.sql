CREATE TABLE [dbo].[Realtors_Master] (
    [user_id] uniqueidentifier  NOT NULL Default NewId(),
    [avatar] nvarchar(254)  NULL,
    [company] nvarchar(128)  NULL,
    [email] nvarchar(128)  NULL,
    [first_name] nvarchar(64)  NOT NULL,
    [preferred] bit  NULL,
    [last_name] nvarchar(64)  NOT NULL,
    [name] nvarchar(129)  NOT NULL,
    [password] nvarchar(64)  NOT NULL,
    [phone] nvarchar(30)  NULL,
    [user_name] nvarchar(64)  NOT NULL,
    [unique_user_key] nvarchar(32)  NOT NULL, 
    CONSTRAINT [PK_Realtors_Master] PRIMARY KEY ([user_id], [last_name], [first_name], [name], [user_name], [unique_user_key], [password])
);