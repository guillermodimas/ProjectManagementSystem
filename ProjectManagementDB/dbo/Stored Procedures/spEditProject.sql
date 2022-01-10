

CREATE PROCEDURE [dbo].[spEditProject]
@ProjectID int,
@ProjectTitle varchar(150),
@ProjectOverview varchar(500),
@ProjectDueDate datetime,
@AssignedCompanyID int
AS
begin
	set nocount on;

		BEGIN TRY
    BEGIN TRANSACTION
		
		  UPDATE [dbo].[Projects] 
		  SET ProjectTitle = @ProjectTitle, 
		  ProjectOverview = @ProjectOverview,
		  ProjectDueDate = @ProjectDueDate,
		  AssignedCompanyID = @AssignedCompanyID
		  WHERE ProjectID = @ProjectID

        COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

		

end