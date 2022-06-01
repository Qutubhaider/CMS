-- =============================================  
-- Author:   Vaibhav Singh
-- Create Date: 11-MAR-22 
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[acceptAssignFile]  
(  
	 @inCaseId INT=NULL,
	 @inIssueFileId INT,
	 @inZoneId INT,
	 @inDivisionId INT,
	 @inDepartmentId INT,
	 @inDesignationId INT,
	 @inStoreFileDetailId INT,
	 @inStatus INT,
	 @stComment NVARCHAR(200),
	 @inAcceptedBy INT,  
	 @inAssignedBy INT,
	 @inSRId INT,
	 @inSuccess INT OUT
)  
AS  
BEGIN TRY  
SET NOCOUNT ON;     
 DECLARE @getDateTimeByTimezone DATETIME =getDate()  
 SET @inSuccess=0 
	BEGIN TRAN  
	
		 IF(ISNULL(@inCaseId,0)=0) 
		 BEGIN 			
				INSERT INTO tblCase(inZoneId,inDivisionId,inDepartmentId,inDesignationId,inStoreFileDetailId,inStatus,stComment,dtCreateDate,inAcceptededBy,inAssignedBy,inSRId)  
				SELECT  @inZoneId,@inDivisionId,@inDepartmentId,@inDesignationId,@inStoreFileDetailId,@inStatus,@stComment ,@getDateTimeByTimezone, @inAcceptedBy , @inAssignedBy, @inSRId 
				SET @inSuccess=101
				
				UPDATE tblIssueFileHistory SET inStatus=1 WHERE inlssueFileId=@inIssueFIleId
		 END 		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH