

CREATE PROCEDURE [dbo].[spDeleteProjectUser]
@ProjectID int,
@UserID uniqueidentifier
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		DELETE FROM [dbo].[ProjectAssignedUsers] 
		WHERE AssignedProjectID = @ProjectID and AssignedUserID = @UserID

        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end