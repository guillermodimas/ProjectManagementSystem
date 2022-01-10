CREATE TABLE [dbo].[Companies] (
    [CompanyID]   INT           IDENTITY (1, 1) NOT NULL,
    [CompanyName] VARCHAR (150) NOT NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC)
);

