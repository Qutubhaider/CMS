CREATE PROCEDURE [dbo].[getSubCategoryDropDown]
(
	@inParentCategory INT
)
AS
	SELECT 
		inCategoryId AS Id,stCategoryName AS Value 
	FROM esuvidhahwh.tblCategoryMaster
	WHERE inParentCategoryId=@inParentCategory
RETURN 0
