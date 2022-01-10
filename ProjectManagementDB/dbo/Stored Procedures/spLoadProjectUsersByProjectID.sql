
CREATE PROCEDURE [dbo].[spLoadProjectUsersByProjectID]
@ProjectID int
AS
begin
	set nocount on;

		SELECT [AssignedUserID] FROM [dbo].[ProjectAssignedUsers]
		WHERE AssignedProjectID = @ProjectID

end