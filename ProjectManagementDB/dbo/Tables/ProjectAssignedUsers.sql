CREATE TABLE [dbo].[ProjectAssignedUsers] (
    [AssignmentID]      INT              IDENTITY (1, 1) NOT NULL,
    [AssignedProjectID] INT              NOT NULL,
    [AssignedUserID]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProjectAssignedUsers] PRIMARY KEY CLUSTERED ([AssignmentID] ASC),
    CONSTRAINT [FK_ProjectAssignedUsers_ToProjects] FOREIGN KEY ([AssignedProjectID]) REFERENCES [dbo].[Projects] ([ProjectID]),
    CONSTRAINT [FK_ProjectAssignedUsers_ToUsers] FOREIGN KEY ([AssignedUserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

