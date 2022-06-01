-- ============================================= 
-- Author:  Vaibhav Singh
-- EXEC getDepartmentList 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getDepartmentList] 
( 
	@stDepartmentName NVARCHAR(211)=NULL,   
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10 ,
	@inUserId INT = NULL
) 
AS 
BEGIN 
SET NOCOUNT ON;   
	SET @stDepartmentName =REPLACE(@stDepartmentName,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'stDepartmentName' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'stDepartmentName'; 
	END 

	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inDepartmentId,unDepartmentId,stDepartmentName
		FROM ( 
            SELECT  
                    D.inDepartmentId, 
                    D.unDepartmentId, 
                    D.stDepartmentName, 
                   
            FROM tblDepartment D WITH(NOLOCK)
			JOIN tblUserProfile UP ON UP.inUserId=D.inCreatedBy
            WHERE 1=1' 
 
	IF(ISNULL(@stDepartmentName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (S.stSectorName LIKE ''%' + CONVERT(NVARCHAR(211), @stDepartmentName)  + '%'')' 

	IF(ISNULL(@inUserId,'')<>'') 
		SET @stSQL = @stSQL + '  AND (UP.inUserId LIKE ''%' + CONVERT(NVARCHAR(211), @inUserId)  + '%'')' 
 
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 