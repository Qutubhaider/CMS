CREATE PROCEDURE [dbo].[getCategoryDropDown]

AS
	SELECT inCategoryId AS Id,stCategoryName AS Value FROM tblCategoryMaster
RETURN 0
