-- =============================================  
-- Author:   Qutub
-- Create Date: 19-May-22 
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/ 

CREATE PROCEDURE [dbo].[saveComplain]
(  															
	@inDepartmentId	INT,																															
	@inCategoryId	INT,													
	@inSubCategoryId INT,																						
	@stComplainText	VARCHAR(200),
	@stUnFileName VARCHAR(200),
	@stFileName VARCHAR(200),
	@inZoneId INT,
	@inDivisionId INT,
	@inStoreId INT,
	@inUserId INT,
	@inSuccess   INT OUT
	)
AS
 BEGIN
  DECLARE @currentDateTime DATETIME=getDate()
  DECLARE @inStoreFileDetailId INT=0
  DECLARE @inServiceRequestId INT=0
  DECLARE @stEmployeeName NVARCHAR(200)
  DECLARE @stPFNumber NVARCHAR(200)
  DECLARE @stEmployeeNumber NVARCHAR(200)
  DECLARE @stPPONumber NVARCHAR(200)
  DECLARE @stMobile NVARCHAR(200)
  DECLARE @inAssignUserId INT
  SET @inSuccess=0
		 BEGIN 	
		 

			   SELECT 
				@stEmployeeName=stFirstName,
				@stPFNumber=stPFNumber,
				@stEmployeeNumber=stEmployeeNumber,
				@stPPONumber=stPPONumber,
				@stMobile=stMobile FROM tblUserDetails where inUserId=@inUserId

		       INSERT INTO tblStoreFileDetails(inUserId,inZoneId,inDivisionId,inDepartmentId,inStoreId,inRoomId,inAlmirahId,inShelvesId
			   ,stFileName,stUnFileName,stEmployeeName,stPFNumber,stEmployeeNumber,stPPONumber,stMobile,inCreatedBy,dtCreateDate) VALUES(@inUserId,@inZoneId,@inDivisionId
			   ,@inDepartmentId,@inStoreId,1,1,1,@stFileName,@stUnFileName,@stEmployeeName,@stPFNumber,@stEmployeeNumber,@stPPONumber
			   ,@stMobile,@inUserId,@currentDateTime)

			   SET @inStoreFileDetailId=SCOPE_IDENTITY()
			   
			   INSERT INTO tblServiceRequest(inStoreFileDetailsId,dtSRdate,inCreatedBy) VALUES (@inStoreFileDetailId,@currentDateTime,@inUserId)

			   SET @inServiceRequestId=SCOPE_IDENTITY()
			   
			   SELECT @inAssignUserId = UP.inUserId FROM tblUserProfile UP
			   JOIN tblUser U ON U.inUserId=UP.inUserId
			   WHERE U.inRole=6 AND UP.inDepartmentId=@inDepartmentId

			   INSERT INTO tblIssueFileHistory(inStoreFileDetailsId,inAssignUserId,inDivisionId,inDepartmentId,dtIssueDate,stComment,inStatus,
												dtCreateDate,inCreatedBy,inSRId,inCategoryId,inSubCategoryId,inUserId)  
			   SELECT  @inStoreFileDetailId,@inAssignUserId,@inDivisionId,@inDepartmentId,@currentDateTime,@stComplainText,1,@currentDateTime, 
												@inUserId , @inServiceRequestId, @inCategoryId, @inSubCategoryId, @inUserId 
			   SET @inSuccess=101  
		 END
 END