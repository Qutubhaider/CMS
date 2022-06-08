CREATE PROCEDURE [dbo].[getCategory]
(
	@inDepartment INT
)
AS
	SELECT 
		inCategoryId AS Id,stCategoryName AS Value 
	FROM esuvidhahwh.tblCategoryMaster
	WHERE inDepartmentId=@inDepartment AND inParentCategoryId=0
RETURN 0