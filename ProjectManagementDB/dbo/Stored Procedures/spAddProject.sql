
CREATE PROCEDURE [dbo].[spAddProject]
@ProjectTitle varchar(150),
@ProjectOverview varchar(500),
@ProjectDueDate datetime,
@AssignedCompanyID int
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		 INSERT INTO [dbo].[Projects] (ProjectTitle, ProjectOverview, ProjectDueDate, AssignedCompanyID)
		 VALUES (@ProjectTitle, @ProjectOverview, @ProjectDueDate, @AssignedCompanyID)


        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end