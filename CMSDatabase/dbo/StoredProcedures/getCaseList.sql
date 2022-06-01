-- ============================================= 
-- Author:  Qutub Haider 
-- EXEC getCaseList
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getCaseList] 
( 
	@stFileName NVARCHAR(211)=NULL, 
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
	ELSE IF @inSortColumn = 2 
	BEGIN 
		SET @stSort = 'inStatus'; 
	END 
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inSRId,inCaseId,unCaseId,stZoneName ,stDivisionName,stDepartmentName,stDesignationName,stFileName,stAssignedBy,inStatus
		FROM ( 
            SELECT  
					C.inSRId,
                    C.inCaseId, 
                    C.unCaseId,  
                    Z.stZoneName,
					DV.stDivisionName,
					DP.stDepartmentName,
					DS.stDesignationName,
					(SF.stEmployeeName+'' || ''+SF.stEmployeeNumber+'' || ''+SF.stPPONumber+'' || ''+SF.stPFNumber) AS stFileName,
					(UP.stFirstName) AS stAssignedBy,
					C.inStatus
            FROM tblCase C WITH(NOLOCK) 
            JOIN tblZone Z ON Z.inZoneId=C.inZoneId
            JOIN tblDivision DV ON DV.inDivisionId=C.inDivisionId
            JOIN tblDepartment DP ON DP.inDepartmentId=C.inDepartmentId
            JOIN tblDesignation DS ON DS.inDesignationId=C.inDesignationId
			JOIN tblStoreFileDetails SF ON SF.inStoreFileDetailsId=C.inStoreFileDetailId
			JOIN tblUserProfile UP ON UP.inUserId=C.inAssignedBy
            WHERE 1=1' 
 
	IF(ISNULL(@stFileName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (SF.stFileName LIKE ''%' + CONVERT(NVARCHAR(211), @stFileName)  + '%'')' 
 
	IF(ISNULL(@inStatus,0)>0)               
		SET @stSQL = @stSQL +' AND C.inStatus= '+ CONVERT(NVARCHAR(11), @inStatus) +'' 
	IF(ISNULL(@inUserId,0)>0)               
		SET @stSQL = @stSQL +' AND C.inAcceptededBy= '+ CONVERT(NVARCHAR(11), @inUserId) +'' 
 
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 
 