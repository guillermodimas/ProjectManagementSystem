
CREATE PROCEDURE [dbo].[spLoadAllProjects]
	
AS
begin
	set nocount on;

		 SELECT [ProjectID] ,[ProjectTitle] ,[ProjectOverview] ,[ProjectDueDate] ,[AssignedCompanyID] 
		 FROM [dbo].[Projects]

end