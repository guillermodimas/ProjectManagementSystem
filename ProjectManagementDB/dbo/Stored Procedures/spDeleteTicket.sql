

CREATE PROCEDURE [dbo].[spDeleteTicket]
@TicketID int
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		 

		 DELETE FROM [dbo].[Tickets]
		 WHERE TicketID = @TicketID


        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end