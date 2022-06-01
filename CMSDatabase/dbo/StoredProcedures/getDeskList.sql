-- ============================================= 
-- Author:  Qutub Haider 
-- EXEC [getDeskList] 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getDeskList] 
( 
	@stDeskName NVARCHAR(211)=NULL, 
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
	SET @stDeskName =REPLACE(@stDeskName,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'stDeskName' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'stDeskName'; 
	END 
	ELSE IF @inSortColumn = 2 
	BEGIN 
		SET @stSort = 'inStatus'; 
	END 
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inDeskId,unDeskId,stDeskName,stZoneName ,stDivisionName,stDepartmentName,stDesignationName
		FROM ( 
            SELECT  
                    D.inDeskId, 
                    D.unDeskId, 
                    D.stDeskName, 
                    Z.stZoneName,
					DV.stDivisionName,
					DP.stDepartmentName,
					DS.stDesignationName
            FROM tblDeskDetail D WITH(NOLOCK) 
            JOIN tblZone Z ON Z.inZoneId=D.inZoneId
            JOIN tblDivision DV ON DV.inDivisionId=D.inDivisionId
            JOIN tblDepartment DP ON DP.inDepartmentId=D.inDepartmentId
            JOIN tblDesignation DS ON DS.inDesignationId=D.inDesignationId
            WHERE 1=1' 
 
	IF(ISNULL(@stDeskName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (D.stDeskName LIKE ''%' + CONVERT(NVARCHAR(211), @stDeskName)  + '%'')' 
 
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
 