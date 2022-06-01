-- ============================================= 
-- Author: Vaibhav Singh
-- EXEC [[getRoomList]] 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getShelveList] 
( 
	@stShelveNumber NVARCHAR(211)=NULL,   
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10 ,
	@inUserId INT=NULL
) 
AS 
BEGIN 
SET NOCOUNT ON;   
	SET @stShelveNumber =REPLACE(@stShelveNumber,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'stShelveNumber' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'stShelveNumber'; 
	END  
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inShelveId,unShelveId,stShelveNumber,stZoneName ,stDivisionName,stDepartmentName,stStoreName,stRoomNumber,stAlmirahNumber
		FROM ( 
            SELECT  
                    S.inShelveId, 
                    S.unShelveId, 
                    S.stShelveNumber, 
                    Z.stZoneName,
					DV.stDivisionName,
					DP.stDepartmentName,
					ST.stStoreName,
					R.stRoomNumber,
					A.stAlmirahNumber
            FROM tblShelve S WITH(NOLOCK) 
            JOIN tblZone Z ON Z.inZoneId=S.inZoneId
            JOIN tblStore ST ON ST.inStoreId=S.inStoreId
            JOIN tblDivision DV ON DV.inDivisionId=S.inDivisionId
            JOIN tblDepartment DP ON DP.inDepartmentId=S.inDepartmentId
            JOIN tblAlmirah A ON A.inAlmirahId=S.inAlmirahId
			JOIN tblRoom R ON R.inRoomId=A.inRoomId
            WHERE 1=1' 
 
	IF(ISNULL(@stShelveNumber,'')<>'') 
		SET @stSQL = @stSQL + '  AND (S.stShelveNumber LIKE ''%' + CONVERT(NVARCHAR(211), @stShelveNumber)  + '%'')' 
 
 +'' 
 IF(ISNULL(@inUserId,0)>0)               
		SET @stSQL = @stSQL +' AND S.inCreatedBy= '+ CONVERT(NVARCHAR(11), @inUserId) +''
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 