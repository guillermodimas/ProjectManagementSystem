

CREATE PROCEDURE [dbo].[spLoadCompanyUsersByCompanyID]
@CompanyID	int
AS
begin
	set nocount on;

		 SELECT [AssignedUserID] FROM [dbo].[CompanyAssignedUsers]
		 WHERE AssignedCompanyID = @CompanyID

end
