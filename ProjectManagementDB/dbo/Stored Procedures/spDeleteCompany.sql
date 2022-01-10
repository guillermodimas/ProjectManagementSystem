
CREATE PROCEDURE [dbo].[spDeleteCompany]
@CompanyID int
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		 DELETE FROM [dbo].[CompanyAssignedUsers]
		 WHERE AssignedCompanyID = @CompanyID

		 DELETE FROM [dbo].[Companies]
		 WHERE CompanyID = @CompanyID


        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end