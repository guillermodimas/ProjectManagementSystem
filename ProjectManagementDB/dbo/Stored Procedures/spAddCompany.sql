CREATE PROCEDURE [dbo].[spAddCompany]
@CompanyName varchar(150)
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		 INSERT INTO [dbo].[Companies] (CompanyName)
		 VALUES(@CompanyName)


        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end