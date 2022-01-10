CREATE PROCEDURE [dbo].[spLoadAllCompanies]
	
AS
begin
	set nocount on;

		 SELECT [CompanyID] ,[CompanyName] FROM [dbo].[Companies] 

end