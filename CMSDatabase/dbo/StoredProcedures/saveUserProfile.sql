-- =============================================  
-- Author:   Qutub Haider  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveUserProfile]  
(  
	 @inUserProfileId INT=NULL,  
	 @inDeskid INT,
	 @inUserId INT,
	 @inZoneId INT,
	 @inStoreId INT,
	 @inDivisionId INT,
	 @inDepartmentId INT,
	 @inDesignationId INT,
	 @stFirstName NVARCHAR(200),
	 @stLastName NVARCHAR(200),
	 @stEmail NVARCHAR(200),
	 @stMobile NVARCHAR(200),
	 @stAddress NVARCHAR(200),
	 @inStatus INT,
	 @inCreatedBy INT,  
	 @inSuccess INT OUT
)  
AS  
BEGIN TRY  
SET NOCOUNT ON;     
 DECLARE @getDateTimeByTimezone DATETIME =getDate()  
 SET @inSuccess=0 
	BEGIN TRAN  
	
		 IF(ISNULL(@inUserProfileId,0)=0)  
		 BEGIN 			
				INSERT INTO tblUserProfile(inDeskid,inUserId,inZoneId,inStoreId, inDivisionId,inDepartmentId
					,inDesignationId,stFirstName,stLastName,stEmail,stMobile,stAddress,inStatus
					,dtCreateDate,inCreatedBy)  
				SELECT  @inDeskid,@inUserId,@inZoneId,@inStoreId,@inDivisionId,@inDepartmentId,@inDesignationId,@stFirstName
					   ,@stLastName,@stEmail,@stMobile,@stAddress,@inStatus,@getDateTimeByTimezone, @inCreatedBy  
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE tblUserProfile WITH(ROWLOCK) SET   
						inDeskid=@inDeskid,
						inUserId=@inUserId,
						inZoneId=@inZoneId,
						inStoreId=@inStoreId,
						inDivisionId=@inDivisionId,
						inDepartmentId=@inDepartmentId,
						inDesignationId=@inDesignationId,
						stFirstName=@stFirstName,
						stLastName=@stLastName,
						stEmail=@stEmail,
						stMobile=@stMobile,
						stAddress=@stAddress,
						inStatus=@inStatus,
						dtCreateDate=@getDateTimeByTimezone,
						inCreatedBy=@inCreatedBy					 
				  WHERE inUserProfileId=@inUserProfileId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH