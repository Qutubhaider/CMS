-- =============================================  
-- Author:   Qutub
-- Create Date: 10-April-22 
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/ 

CREATE PROCEDURE [dbo].[saveIssueFileByStore]
(  
	@inIssueFileId	INT=NULL,	
	@inSRId INT,
	@inStoreFileId	INT,																
	@inUserId		INT,																															
	@inDivisionId	INT,													
	@inDepartmentId	INT,																						
	@stComment		VARCHAR(200),
	@inStatus       INT,
	@inCreatedBy    INT,
	@inSuccess      INT OUT
)
AS
 BEGIN
  DECLARE @currentDateTime DATETIME=getDate()
  DECLARE @inCategoryId INT
  DECLARE @inSubCategoryId INT
  DECLARE @inOldUserId INT
  SET @inSuccess=0
  SET @inCategoryId=0
  SET @inSubCategoryId=0
  SET @inUserId=0
		 BEGIN 	

				SELECT 
					@inCategoryId=inCategoryId, 
					@inSubCategoryId=inSubCategoryId, 
					@inOldUserId=inCreatedBy
				FROM tblIssueFileHistory
				WHERE inSRId=@inSRId
				
				UPDATE tblIssueFileHistory SET inStatus=2 WHERE inlssueFileId = @inIssueFileId

				INSERT INTO tblIssueFileHistory(inStoreFileDetailsId,inAssignUserId,inDivisionId,inDepartmentId,dtIssueDate,stComment,inStatus,
												dtCreateDate,inCreatedBy,inSRId,inCategoryId,inSubCategoryId,inUserId)  
				SELECT  @inStoreFileId,@inUserId,@inDivisionId,@inDepartmentId,GETDATE(),@stComment,1,@currentDateTime, 
												@inCreatedBy , @inSRId, @inCategoryId, @inSubCategoryId, @inOldUserId
				SET @inSuccess=101  
		 END
 END
