-- ============================================= 
-- Author: Vaibhav Singh
-- EXEC getUserList
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 
CREATE PROC [dbo].[getUserList] 
( 
	@inDepartmentId INT=NULL,
	@inDivisionId INT=NULL,
	@stUserName NVARCHAR(211)=NULL,   
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10 ,
	@inUserId INT=NULL
) 
AS 
BEGIN 
SET NOCOUNT ON;   
	SET @stUserName =REPLACE(@stUserName,'''','''''') 
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'stUserName' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'stUserName'; 
	END  
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		stUserName, inStatus ,unUserProfileId,stZoneName,stDivisionName,stDepartmentName,stDesignationName,stDeskName,inRole
		FROM ( 
            SELECT  
                    UP.stFirstName AS stUserName,
					UP.inStatus,
					U.inRole,
					UP.unUserProfileId,
					Z.stZoneName,
					DV.stDivisionName,
					DP.stDepartmentName,
					DS.stDesignationName,
					DD.stDeskName
            FROM tblUserProfile UP WITH(NOLOCK) 
			JOIN tblUser U on U.inUserId=UP.inUserId
            JOIN tblZone Z ON Z.inZoneId=UP.inZoneId
            JOIN tblDivision DV ON DV.inDivisionId=UP.inDivisionId
            JOIN tblDepartment DP ON DP.inDepartmentId=UP.inDepartmentId
            JOIN tblDesignation DS ON DS.inDesignationId=UP.inDesignationId
            JOIN tblDeskDetail DD ON DD.inDeskId = UP.inDeskId
            WHERE 1=1' 
 
 IF(ISNULL(@stUserName,'')<>'') 
		SET @stSQL = @stSQL + '  AND (UP.stFirstName LIKE ''%' + CONVERT(NVARCHAR(211), @stUserName)  + '%'')' +'' 



 IF(ISNULL(@inDepartmentId,0)>0)               
		SET @stSQL = @stSQL +' AND UP.inDepartmentId= '+ CONVERT(NVARCHAR(11), @inDepartmentId) +''
 ELSE  IF(ISNULL(@inUserId,0)>0)               
		SET @stSQL = @stSQL +' AND UP.inCreatedBy= '+ CONVERT(NVARCHAR(11), @inUserId) +''
 
 IF(ISNULL(@inDivisionId,0)>0)               
		SET @stSQL = @stSQL +' AND UP.inDivisionId= '+ CONVERT(NVARCHAR(11), @inDivisionId) +''

 
 SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END 
