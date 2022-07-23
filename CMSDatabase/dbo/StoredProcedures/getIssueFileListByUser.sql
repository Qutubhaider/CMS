-- ============================================= 
-- Author: Qutub
-- EXEC getIssueFileListByUser 
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getIssueFileListByUser] 
( 
	@stFileName NVARCHAR(211)=NULL,   
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10 ,
	@inCreatedBy INT = NULL,
	@inUserId INT = NULL,
	@inDepartmentId INT = NULL,
	@inDivisionId INT = NULL
) 
AS 
BEGIN 
SET NOCOUNT ON;   
	SET @stFileName =REPLACE(@stFileName,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'inlssueFileId' 
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
		inSRId,inlssueFileId,unlssueFileId,dtIssueDate,stComment,inStatus,stFileName,stDivisionName,
		stFirstNameAssignedBy,stDepartmentAssignedBy,stFirstNameAssignTo,stDepartmentAssignedTo,stCategoryName,stSubCategoryName,AGEING
		FROM ( 
            SELECT  IFH.inSRId,
                    IFH.inlssueFileId, 
                    IFH.unlssueFileId, 
					IFH.dtIssueDate,
					IFH.stComment,
					IFH.inStatus,
                    (F.stEmployeeName) AS stFileName,
					DV.stDivisionName,					
					(UPS.stFirstName) AS stFirstNameAssignedBy,
					(DP.stDepartmentName) stDepartmentAssignedBy,
					(UPF.stFirstName) AS stFirstNameAssignTo,
					(DPS.stDepartmentName) stDepartmentAssignedTo,
					CM.stCategoryName,
					(CMS.stCategoryName) AS stSubCategoryName,
					DATEDIFF(day,dtIssueDate,getdate() ) AS AGEING
            FROM tblIssueFileHistory IFH WITH(NOLOCK)
            JOIN tblDivision DV ON DV.inDivisionId=IFH.inDivisionId
			LEFT JOIN tblUserProfile UPS ON UPS.inUserId=IFH.inAssignUserId
			LEFT JOIN tblUserDetails UPF ON UPF.inUserId=IFH.inCreatedBy
            JOIN tblStoreFileDetails F ON F.inStoreFileDetailsId=IFH.inStoreFileDetailsId
            LEFT JOIN tblDepartment DP ON DP.inDepartmentId=UPS.inDepartmentId
            LEFT JOIN tblDepartment DPS ON DPS.inDepartmentId=UPF.inDepartmentId
		    JOIN tblCategoryMaster CM ON CM.inCategoryId=IFH.inCategoryId
			JOIN tblCategoryMaster CMS ON CMS.inCategoryId=IFH.inSubCategoryId
			WHERE 1=1' 
 
	IF(ISNULL(@stFileName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (F.stFileName LIKE ''%' + CONVERT(NVARCHAR(211), @stFileName)  + '%'')' 
 
 +'' 

 IF(ISNULL(@inCreatedBy,0)>0)               
		SET @stSQL = @stSQL +' AND IFH.inCreatedBy= '+ CONVERT(NVARCHAR(11), @inCreatedBy) +''

 IF(ISNULL(@inUserId,0)>0)               
		SET @stSQL = @stSQL +' AND IFH.inUserId= '+ CONVERT(NVARCHAR(11), @inUserId) +''



	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 
