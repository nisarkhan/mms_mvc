CREATE TABLE [dbo].[LOS_LoanOfficer_Lookup] (
    [LOS_LoanOfficer_Id] uniqueidentifier  NOT NULL,
    [Avatar] nvarchar(254)  NULL,
    [Email] nvarchar(254)  NULL,
    [EmployeeNumber] nvarchar(64)  NULL,
    [Fax] nvarchar(15)  NULL,
    [FirstName] nvarchar(64)  NULL,
    [LastName] nvarchar(64)  NULL,
    [Name] nvarchar(128)  NULL,
    [Phone] nvarchar(15)  NULL,
    [NMLS] nvarchar(64)  NULL,
    [URL_Web] nvarchar(254)  NULL, 
    CONSTRAINT [PK_LOS_LoanOfficer_Lookup] PRIMARY KEY ([LOS_LoanOfficer_Id])
);