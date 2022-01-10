
CREATE PROCEDURE [dbo].[spDeleteCompanyUser]
@CompanyID int,
@UserID uniqueidentifier
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		DELETE FROM [dbo].[CompanyAssignedUsers]
		WHERE AssignedCompanyID = @CompanyID and AssignedUserID = @UserID

        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end