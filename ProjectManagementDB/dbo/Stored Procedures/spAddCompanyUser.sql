

CREATE PROCEDURE [dbo].[spAddCompanyUser]
@CompanyID int,
@UserID uniqueidentifier
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		INSERT INTO [dbo].[CompanyAssignedUsers] (AssignedCompanyID, AssignedUserID)
		VALUES(@CompanyID, @UserID)

        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end