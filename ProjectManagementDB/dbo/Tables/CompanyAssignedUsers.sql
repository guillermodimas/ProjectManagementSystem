CREATE TABLE [dbo].[CompanyAssignedUsers] (
    [AssignmentID]      INT              IDENTITY (1, 1) NOT NULL,
    [AssignedCompanyID] INT              NOT NULL,
    [AssignedUserID]    UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([AssignmentID] ASC),
    CONSTRAINT [FK_CompanyAssignedUsers_ToCompanies] FOREIGN KEY ([AssignedCompanyID]) REFERENCES [dbo].[Companies] ([CompanyID]),
    CONSTRAINT [FK_CompanyAssignedUsers_ToUsers] FOREIGN KEY ([AssignedUserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

