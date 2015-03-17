CREATE TABLE [dbo].[LoginHistories] (
    [LoginHistory_Id] uniqueidentifier  NOT NULL,
    [LoanOfficerEmployeeNumber] nvarchar(64)  NULL,
    [LoanOfficerFirstName] nvarchar(64)  NULL,
    [LoanOfficerLastName] nvarchar(64)  NULL,
    [LoginDatetime] datetime  NOT NULL,
    [UserName] nvarchar(64)  NOT NULL, 
    CONSTRAINT [PK_LoginHistories] PRIMARY KEY ([LoginDatetime], [UserName], [LoginHistory_Id])
);
GO