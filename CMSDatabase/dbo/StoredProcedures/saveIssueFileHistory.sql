-- =============================================  
-- Author:   Vaibhav Singh
-- Create Date: 11-MAR-22 
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/ 

CREATE PROCEDURE [dbo].[saveIssueFileHistory]
(  
@inIssueFileId	INT=NULL,																																	
@inStoreFileId			INT,																
@inUserId			INT,																															
@inDivisionId		INT,													
@inDepartmentId		INT,																					
@stComment		NVARCHAR(200),
@stFileName		NVARCHAR(200),
@stUnFileName		NVARCHAR(200),
@inStatus           INT,
@inCreatedBy        INT,
@inSuccess INT OUT,
@inSRId INT,
@inCaseId INT
)
AS
 BEGIN
  DECLARE @currentDateTime DATETIME=getDate()
  SET @inSuccess=0
	IF(ISNULL(@inIssueFileId,0)=0)  
		 BEGIN 			
				INSERT INTO tblIssueFileHistory(inStoreFileDetailsId,inAssignUserId,inDivisionId,inDepartmentId,dtIssueDate,stComment,inStatus,dtCreateDate,inCreatedBy,inSRId,stFileName,stUnFileName)  
				SELECT  @inStoreFileId,@inUserId,@inDivisionId,@inDepartmentId,GETDATE(),@stComment,@inStatus,@currentDateTime, @inCreatedBy  ,@inSRId,@stFileName,@stUnFileName
				SET @inSuccess=101  

				UPDATE tblCase SET inStatus=2 WHERE inCaseId=@inCaseId
		 END
    ELSE
		BEGIN
		UPDATE tblIssueFileHistory
		SET
		inAssignUserId        =     @inUserId		,
		inDivisionId 			=   @inDivisionId	,
		inDepartmentId			=   @inDepartmentId	,
		inStoreFileDetailsId	 =  @inStoreFileId	,
		dtIssueDate 			=  GETDATE()		,
		stComment 				 = @stComment		,
		inStatus 				=  @inStatus   		,       			  
		dtCreateDate    =          @currentDateTime,
		inCreatedBy =@inCreatedBy,
		inSRId=@inSRId
		WHERE inlssueFileId=@inIssueFileId
		SET @inSuccess=102
		END
 END
