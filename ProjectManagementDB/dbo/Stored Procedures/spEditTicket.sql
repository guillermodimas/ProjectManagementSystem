



CREATE PROCEDURE [dbo].[spEditTicket]
@TicketID int,
@Status int,
@Title varchar(100),
@Description varchar(500),
@AssignedProjectID int,
@AssignedUserID uniqueidentifier
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		 UPDATE [dbo].[Tickets] 
		 SET 
			 Status = @Status, 
			 Title = @Title, 
			 Description = @Description, 
			 LastUpdated = GETDATE(), 
			 AssignedProjectID = @AssignedProjectID, 
			 AssignedUserID = @AssignedUserID
		 WHERE TicketID = @TicketID

        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end