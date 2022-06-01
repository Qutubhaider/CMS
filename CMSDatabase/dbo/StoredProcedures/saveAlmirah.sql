-- =============================================  
-- Author:   Vaibhav Singh
-- Create Date: 11-MAR-22 
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveAlmirah]  
(  
	 @inAmirahId INT=NULL,  
	 @stAmirahNumber nvarchar(200),
	 @inZoneId INT,
	 @inDivisionId INT,
	 @inDepartmentId INT,
	 @inStoreId INT,
	 @inRoomId INT,
	 @inCreatedBy INT,  
	 @inSuccess INT OUT
)  
AS  
BEGIN TRY  
SET NOCOUNT ON;     
 DECLARE @getDateTimeByTimezone DATETIME =getDate()  
 SET @inSuccess=0 
	BEGIN TRAN  
	
		 IF(ISNULL(@inAmirahId,0)=0)  
		 BEGIN 			
				INSERT INTO tblAlmirah(stAlmirahNumber,inZoneId,inDivisionId,inDepartmentId,inStoreId,inRoomId,dtCreateDate,inCreatedBy)  
				SELECT  @stAmirahNumber, @inZoneId,@inDivisionId,@inDepartmentId,@inStoreId,@inRoomId ,@getDateTimeByTimezone, @inCreatedBy  
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE tblAlmirah WITH(ROWLOCK) SET   
						stAlmirahNumber=@stAmirahNumber,
						inZoneId=@inZoneId,
						inDivisionId=@inDivisionId,
						inDepartmentId= @inDepartmentId,
						inStoreId=@inStoreId,
						inRoomId=@inRoomId,
						dtCreateDate=@getDateTimeByTimezone,
						inCreatedBy=@inCreatedBy					 
				  WHERE inAlmirahId=@inAmirahId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH