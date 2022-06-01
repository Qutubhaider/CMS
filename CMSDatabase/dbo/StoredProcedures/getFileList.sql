-- ============================================= 
-- Author: Vaibhav Singh
-- EXEC getFileList 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getFileList] 
( 
	@stFileName NVARCHAR(211)=NULL,   
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10 ,
	@inUserId INT=NULL
) 
AS 
BEGIN 
SET NOCOUNT ON;   
	SET @stFileName =REPLACE(@stFileName,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'stFileName' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'stFileName'; 
	END  
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inStoreFileDetailsId,unStoreFileDetailsId,stFileName,stUnFileName,stEmployeeName,stPPONumber,stPFNumber,stEmployeeNumber,stMobile,stShelveNumber,stZoneName,stDivisionName,stDepartmentName,stStoreName,stRoomNumber,stAlmirahNumber
		FROM ( 
            SELECT  
                    F.inStoreFileDetailsId, 
                    F.unStoreFileDetailsId, 
                    F.stFileName,
					F.stUnFileName,
					F.stEmployeeName,
					F.stPPONumber,
					F.stPFNumber,
					F.stEmployeeNumber,
					F.stMobile,
					S.stShelveNumber, 
                    Z.stZoneName,
					DV.stDivisionName,
					DP.stDepartmentName,
					ST.stStoreName,
					R.stRoomNumber,
					A.stAlmirahNumber
                    
            FROM tblStoreFileDetails F WITH(NOLOCK)
			JOIN tblZone Z ON Z.inZoneId=F.inZoneId
            JOIN tblStore ST ON ST.inStoreId=F.inStoreId
            JOIN tblDivision DV ON DV.inDivisionId=F.inDivisionId
            JOIN tblDepartment DP ON DP.inDepartmentId=F.inDepartmentId
            JOIN tblAlmirah A ON A.inAlmirahId=F.inAlmirahId
			JOIN tblRoom R ON R.inRoomId=F.inRoomId
			JOIN tblShelve S ON S.inShelveId=F.inShelvesId
            
            WHERE 1=1' 
 
	IF(ISNULL(@stFileName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (F.stFileName LIKE ''%' + CONVERT(NVARCHAR(211), @stFileName)  + '%'')' 
 
 +'' 
 IF(ISNULL(@inUserId,0)>0)               
		SET @stSQL = @stSQL +' AND F.inCreatedBy= '+ CONVERT(NVARCHAR(11), @inUserId) +''
 
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 