CREATE TABLE [dbo].[Realtors_Extension] (
    [Realtor_Extension_Id] uniqueidentifier  NOT NULL,
    [User_Id] uniqueidentifier  NOT NULL,
    [Avatar] nvarchar(254)  NULL,
    [CompanyName] nvarchar(128)  NULL,
    [IsPreferredIndicator] bit  NOT NULL, 
    CONSTRAINT [PK_Realtors_Extension] PRIMARY KEY ([Realtor_Extension_Id])
);