
CREATE PROCEDURE [dbo].[spLoadAllTickets]
	
AS
begin
	set nocount on;

		 SELECT [TicketID] ,[Status] ,[Title] ,[Description] ,[LastUpdated] ,[AssignedProjectID] ,[AssignedUserID] 
		 FROM [dbo].[Tickets] 

end