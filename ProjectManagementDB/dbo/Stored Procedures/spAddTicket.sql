


CREATE PROCEDURE [dbo].[spAddTicket]
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
		
		 INSERT INTO [dbo].[Tickets] (Status, Title, Description, LastUpdated, AssignedProjectID, AssignedUserID)
		 VALUES(@Status, @Title,@Description, GETDATE() , @AssignedProjectID, @AssignedUserID )

        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end