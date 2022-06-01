-- ============================================= 
-- Author: Vaibhav Singh
-- EXEC getFileList 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getIssueFileList] 
( 
	@stFileName NVARCHAR(211)=NULL,   
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10 ,
	@inUserId INT = NULL,
	@inDepartmentId INT = NULL,
	@inDivisionId INT = NULL
) 
AS 
BEGIN 
SET NOCOUNT ON;   
	SET @stFileName =REPLACE(@stFileName,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'dtIssueDate' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'stFileName'; 
	END  
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY dtIssueDate DESC) AS INT) AS inRownumber, 
		inSRId,inlssueFileId,unlssueFileId,dtIssueDate,stComment,inStatus,stFileName,stDivisionName,stFirstNameAssignedBy,stDepartmentAssignedBy,stFirstNameAssignTo,stDepartmentAssignedTo
		FROM ( 
            SELECT  IFH.inSRId,
                    IFH.inlssueFileId, 
                    IFH.unlssueFileId, 
					IFH.dtIssueDate,
					IFH.stComment,
					IFH.inStatus,
                    (F.stEmployeeName+'' || ''+F.stEmployeeNumber+'' || ''+F.stPPONumber+'' || ''+F.stPFNumber) AS stFileName,
					DV.stDivisionName,					
					(UP.stFirstName) AS stFirstNameAssignedBy,
					(DP.stDepartmentName) stDepartmentAssignedBy,
					(UPF.stFirstName) AS stFirstNameAssignTo,
					(DPS.stDepartmentName) stDepartmentAssignedTo
            FROM tblIssueFileHistory IFH WITH(NOLOCK)
            JOIN tblDivision DV ON DV.inDivisionId=IFH.inDivisionId
            JOIN tblUserProfile UP ON UP.inUserProfileId=IFH.inCreatedBy
			JOIN tblUserProfile UPF ON UPF.inUserProfileId=IFH.inAssignUserId
            JOIN tblStoreFileDetails F ON F.inStoreFileDetailsId=IFH.inStoreFileDetailsId
            JOIN tblDepartment DP ON DP.inDepartmentId=UP.inDepartmentId
            JOIN tblDepartment DPS ON DPS.inDepartmentId=UPF.inDepartmentId
            WHERE 1=1' 
 
	IF(ISNULL(@stFileName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (F.stFileName LIKE ''%' + CONVERT(NVARCHAR(211), @stFileName)  + '%'')' 
 
 +'' 
 IF(ISNULL(@inUserId,0)>0)               
		SET @stSQL = @stSQL +' AND IFH.inAssignUserId= '+ CONVERT(NVARCHAR(11), @inUserId) +''
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 
