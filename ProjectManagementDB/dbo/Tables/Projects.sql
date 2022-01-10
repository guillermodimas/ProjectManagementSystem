CREATE TABLE [dbo].[Projects] (
    [ProjectID]         INT            IDENTITY (1, 1) NOT NULL,
    [ProjectTitle]      VARCHAR (150)  NOT NULL,
    [ProjectOverview]   VARCHAR (5000) NOT NULL,
    [ProjectDueDate]    DATETIME       NOT NULL,
    [AssignedCompanyID] INT            NULL,
    PRIMARY KEY CLUSTERED ([ProjectID] ASC)
);

