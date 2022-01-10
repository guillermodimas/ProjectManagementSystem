

CREATE PROCEDURE [dbo].[spAddProjectUser]
@ProjectID int,
@UserID uniqueidentifier
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		INSERT INTO [dbo].[ProjectAssignedUsers] (AssignedProjectID, AssignedUserID)
		VALUES (@ProjectID, @UserID)

        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end