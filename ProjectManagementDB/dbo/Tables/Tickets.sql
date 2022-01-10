CREATE TABLE [dbo].[Tickets] (
    [TicketID]          INT              IDENTITY (1, 1) NOT NULL,
    [Status]            INT              NOT NULL,
    [Title]             VARCHAR (100)    NOT NULL,
    [Description]       VARCHAR (500)    NOT NULL,
    [LastUpdated]       DATETIME         NOT NULL,
    [AssignedProjectID] INT              NOT NULL,
    [AssignedUserID]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([TicketID] ASC),
    CONSTRAINT [FK_Tickets_ToProjects] FOREIGN KEY ([AssignedProjectID]) REFERENCES [dbo].[Projects] ([ProjectID])
);



