

CREATE PROCEDURE [dbo].[spDeleteProject]
@ProjectID int
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		 DELETE FROM [dbo].[ProjectAssignedUsers]
		 WHERE AssignedProjectID = @ProjectID

		 DELETE FROM [dbo].[Projects]
		 WHERE ProjectID = @ProjectID


        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end