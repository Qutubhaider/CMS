CREATE PROCEDURE [dbo].[getCategoryDropDown]
(
	@inDepartmentId INT
)
AS
	SELECT inCategoryId AS Id,stCategoryName AS Value FROM [esuvidhahwh].[tblCategoryMaster]
	WHERE inDepartmentId=@inDepartmentId AND inParentCategoryId=0
