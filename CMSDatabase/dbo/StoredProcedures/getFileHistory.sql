-- ============================================= 
-- Author: Qutub
-- EXEC getFileHistory 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getFileHistory] 
( 
	@inSRId INT
) 
AS 
BEGIN 
SET NOCOUNT ON; 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'inSRId' 
	DECLARE @inStart INT, @inEnd INT 
	DECLARE @inSortColumn INT = 1
	DECLARE @stSortOrder NVARCHAR(51) = 'DESC'
	DECLARE @inPageNo INT = 1
	DECLARE @inPageSize INT = 10 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'inSRId'; 
	END  
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inSRId,inlssueFileId,unlssueFileId,dtIssueDate,stComment,inStatus,stFileName,stDivisionName,stDepartmentName,stFirstName
		FROM ( 
            SELECT  IFH.inSRId,
                    IFH.inlssueFileId, 
                    IFH.unlssueFileId, 
					IFH.dtIssueDate,
					IFH.stComment,
					IFH.inStatus,
                    (F.stEmployeeName+'' || ''+F.stEmployeeNumber+'' || ''+F.stPPONumber+'' || ''+F.stPFNumber) AS stFileName,
					DV.stDivisionName,
					DP.stDepartmentName,
					UP.stFirstName
            FROM tblIssueFileHistory IFH WITH(NOLOCK)
            JOIN tblDivision DV ON DV.inDivisionId=IFH.inDivisionId
            JOIN tblUserProfile UP ON UP.inUserProfileId=IFH.inCreatedBy
            JOIN tblStoreFileDetails F ON F.inStoreFileDetailsId=IFH.inStoreFileDetailsId
            JOIN tblDepartment DP ON DP.inDepartmentId=IFH.inDepartmentId
            
            WHERE 1=1' 
 
	
 IF(ISNULL(@inSRId,0)>0)               
		SET @stSQL = @stSQL +' AND IFH.inSRId= '+ CONVERT(NVARCHAR(11), @inSRId) +''
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 