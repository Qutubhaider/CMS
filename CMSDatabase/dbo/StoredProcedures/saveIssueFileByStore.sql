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
  SET @inSuccess=0
		 BEGIN 	

				INSERT INTO tblServiceRequest(inStoreFileDetailsId,dtSRdate,inCreatedBy) VALUES (@inStoreFileId,@currentDateTime,@inCreatedBy)

				INSERT INTO tblIssueFileHistory(inStoreFileDetailsId,inAssignUserId,inDivisionId,inDepartmentId,dtIssueDate,stComment,inStatus,
												dtCreateDate,inCreatedBy,inSRId)  
				SELECT  @inStoreFileId,@inUserId,@inDivisionId,@inDepartmentId,GETDATE(),@stComment,@inStatus,@currentDateTime, 
												@inCreatedBy , SCOPE_IDENTITY() 
				SET @inSuccess=101  
		 END
 END
