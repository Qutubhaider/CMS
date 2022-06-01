-- =============================================  
-- Author:   Qutub
-- Create Date: 17-April-22 
-- =============================================
-- EXEC getTraceFile 3
/*  
Ref# Modified By   Modified date   Description  
*/ 

CREATE PROCEDURE getTraceFile
(
	@inDivisionId INT=NULL,
	@inStoreId INT=NULL,
	@inSRId INT=NULL,
	@stEmployeeNo NVARCHAR(211)=NULL,  
	@stPPONo NVARCHAR(211)=NULL,  
	@stPFNo NVARCHAR(211)=NULL,  
	@stMobile NVARCHAR(211)=NULL,  
	@inSortColumn INT = NULL, 
	@stSortOrder NVARCHAR(51) = NULL, 
	@inPageNo INT = 1, 
	@inPageSize INT = 10
		
)
AS
BEGIN
	SET NOCOUNT ON;  
	DECLARE @stSQL AS NVARCHAR(MAX) 
	DECLARE @stSort AS NVARCHAR(MAX) = 'inSRId' 
	DECLARE @inStart INT, @inEnd INT 
 
	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC') 
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1 
	SET @inEnd  = @inPageNo * @inPageSize 
 
	IF @inSortColumn = 1 
	BEGIN 
		SET @stSort = 'inSRId'; 
	END  
	SET @stSQL=''+'WITH PAGED AS(  
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber, 
		inSRId,unSRId,stEmployeeName,stEmployeeNumber,stPFNumber,stPPONumber,stMobile,inStatus,dtSRdate
		FROM ( 
            SELECT 
				S.inSRId,
				S.unSRId,
				SFD.stEmployeeName,
				SFD.stEmployeeNumber,
				SFD.stPFNumber,
				SFD.stPPONumber,
				SFD.stMobile,
				SFD.inStatus,
				S.dtSRdate
			FROM tblServiceRequest S
			JOIN tblStoreFileDetails SFD ON SFD.inStoreFileDetailsId=S.inStoreFileDetailsId
            WHERE 1=1' 

  IF(ISNULL(@inDivisionId,0)>0)               
		SET @stSQL = @stSQL +' AND SFD.inDivisionId= '+ CONVERT(NVARCHAR(11), @inDivisionId) +''

  IF(ISNULL(@inStoreId,0)>0)               
		SET @stSQL = @stSQL +' AND SFD.inStoreId= '+ CONVERT(NVARCHAR(11), @inStoreId) +''

  IF(ISNULL(@stEmployeeNo,'')<>'') 
		SET @stSQL = @stSQL + '  AND (SFD.stEmployeeNo LIKE ''%' + CONVERT(NVARCHAR(211), @stEmployeeNo)  + '%'')'+'' 

  IF(ISNULL(@inSRId,0)>0)               
		SET @stSQL = @stSQL +' AND S.inSRId= '+ CONVERT(NVARCHAR(11), @inSRId) +''
 
	SET @stSQL = @stSQL +' 
				)A )   
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*   
				FROM PAGED '  
					 
	SET @stSQL = @stSQL + '	 
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' ' 
 
	PRINT(@stSQL) 
	EXEC (@stSQL) 
END
