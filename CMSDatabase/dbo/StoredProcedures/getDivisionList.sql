-- ============================================= 
-- Author:  Qutub Haider 
-- EXEC getAllDivision 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getDivisionList] 
( 
	@stDivistionName NVARCHAR(211)=NULL, 
	@inStatus INT=NULL,   
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10,
	@inUserId INT = NULL
) 
AS 
BEGIN 
SET NOCOUNT ON;   
	SET @stDivistionName =REPLACE(@stDivistionName,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'stDivisionName' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'stDivisionName'; 
	END 
	ELSE IF @inSortColumn = 2 
	BEGIN 
		SET @stSort = 'inStatus'; 
	END 
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inDivisionId,unDivisionId,stDivisionName,stZoneName 
		FROM ( 
            SELECT  
                    D.inDivisionId, 
                    D.unDivisionId, 
                    D.stDivisionName, 
                    Z.stZoneName
            FROM tblDivision D WITH(NOLOCK) 
            JOIN tblZone Z ON Z.inZoneId=D.inZoneId
            WHERE 1=1' 
 
	IF(ISNULL(@stDivistionName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (D.stDivisionName LIKE ''%' + CONVERT(NVARCHAR(211), @stDivistionName)  + '%'')' 
 
	IF(ISNULL(@inStatus,0)>0)               
		SET @stSQL = @stSQL +' AND D.inStatus= '+ CONVERT(NVARCHAR(11), @inStatus) +''

	IF(ISNULL(@inUserId,0)>0)               
		SET @stSQL = @stSQL +' AND D.inCreatedBy= '+ CONVERT(NVARCHAR(11), @inUserId) +'' 
 
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 
 