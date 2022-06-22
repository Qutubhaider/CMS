CREATE PROC getCategoryDetail
(
	@unCategoryId UNIQUEIDENTIFIER
)
AS BEGIN
	SELECT
		inCategoryId,
		unCategoryId,
		stCategoryName,
		inParentCategoryId,
		inDepartmentId,
		inStatus,
		inCreatedBy
    FROM tblCategoryMaster 
	WHERE unCategoryId=@unCategoryId
END