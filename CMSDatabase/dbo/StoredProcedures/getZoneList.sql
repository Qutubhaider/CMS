-- =============================================
-- Author: Vaibhav Singh
-- EXEC getState
-- =============================================
/*
Ref#	Modified By			Modified date			Description
*/
CREATE PROC [dbo].[getZoneList]
(
	@stZoneName NVARCHAR(211)=NULL,
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
	SET @stZoneName =REPLACE(@stZoneName,'''','''''')
	DECLARE @stSQL AS NVARCHAR(MAX)
	DECLARE @stSort AS NVARCHAR(MAX) = 'stStateName'
	DECLARE @inStart INT, @inEnd INT

	SET @stSortOrder = ISNULL(@stSortOrder, 'DESC')
	SET @inStart  = (@inPageNo - 1) * @inPageSize + 1
	SET @inEnd  = @inPageNo * @inPageSize

	IF @inSortColumn = 1
	BEGIN
		SET @stSort = 'stZoneName';
	END
	ELSE IF @inSortColumn = 2
	BEGIN
		SET @stSort = 'inStatus';
	END
	SET @stSQL=''+'WITH PAGED AS( 
		SELECT CAST(ROW_NUMBER() OVER(ORDER BY '+ @stSort + ' ' + ISNULL(@stSortOrder,'ASC') + ' ) AS INT) AS inRownumber,
		inZoneId,unZoneId,stZoneName
		FROM (
	SELECT 
		Z.inZoneId,
		Z.unZoneId,
		Z.stZoneName
	FROM tblZone Z WITH(NOLOCK)
	WHERE 1=1'

	IF(ISNULL(@stZoneName,'')<>'')
		SET @stSQL = @stSQL + '  AND (Z.stZoneName LIKE ''%' + CONVERT(NVARCHAR(211), @stZoneName)  + '%'')'

	IF(ISNULL(@inStatus,0)>0)              
		SET @stSQL = @stSQL +' AND S.inStatus= '+ CONVERT(NVARCHAR(11), @inStatus) +''
		IF(ISNULL(@inUserId,0)>0)               
		SET @stSQL = @stSQL +' AND Z.inCreatedBy= '+ CONVERT(NVARCHAR(11), @inUserId) +''

	SET @stSQL = @stSQL +'
				)A )  
				SELECT (SELECT CAST(COUNT(*) AS INT) FROM PAGED) AS inRecordCount,*  
				FROM PAGED ' 
					
	SET @stSQL = @stSQL + '	
				WHERE PAGED.inRownumber BETWEEN ' + CONVERT(NVARCHAR(11), @inStart) + ' AND ' + CONVERT(NVARCHAR(11), @inEnd) + ' '

	PRINT(@stSQL)
	EXEC (@stSQL)
END




