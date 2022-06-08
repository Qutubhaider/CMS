-- ============================================= 
-- Author:  Vaibhav Singh
-- EXEC getDepartmentList 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getCategoryList] 
( 
	@stCategoryName NVARCHAR(211)=NULL,   
	@inStatus INT=NULL,
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10 ,
	@inUserId INT = NULL
) 
AS 
BEGIN 
SET NOCOUNT ON;   
	SET @stCategoryName =REPLACE(@stCategoryName,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'stCategoryName' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'stCategoryName'; 
	END 

	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inCategoryId,unCategoryId,stCategoryName,stDepartmentName,inStatus,stParentCategoryName
		FROM ( 
            SELECT  
                    C.inCategoryId, 
                    C.unCategoryId, 
                    C.stCategoryName, 
					(PC.stCategoryName) AS stParentCategoryName,
					D.stDepartmentName,
					C.inStatus
            FROM tblCategoryMaster C WITH(NOLOCK)
			JOIN tblDepartment D ON C.inDepartmentId=D.inDepartmentId
			LEFT JOIN tblCategoryMaster PC ON C.inParentCategoryId = PC.inCategoryId
            WHERE 1=1' 
 
	IF(ISNULL(@stCategoryName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (C.stCategoryName LIKE ''%' + CONVERT(NVARCHAR(211), @stCategoryName)  + '%'')' 

 
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 