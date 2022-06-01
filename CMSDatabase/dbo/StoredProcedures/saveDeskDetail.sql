-- =============================================  
-- Author:   Qutub Haider  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveDeskDetail]  
(  
	 @inDeskId INT=NULL,  
	 @stDeskName nvarchar(200),
	 @inZoneId INT,
	 @inDivisionId INT,
	 @inDepartmentId INT,
	 @inDesignationId INT,
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
	
		 IF(ISNULL(@inDeskId,0)=0)  
		 BEGIN 			
				INSERT INTO tblDeskDetail(stDeskName,inZoneId,inDivisionId,inDepartmentId,inDesignationId,inStatus, dtCreateDate,inCreatedBy)  
				SELECT  @stDeskName, @inZoneId,@inDivisionId,@inDepartmentId,@inDesignationId,@inStatus, @getDateTimeByTimezone, @inCreatedBy  
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE tblDeskDetail WITH(ROWLOCK) SET   
						stDeskName=@stDeskName,
						inZoneId=@inZoneId,
						inDivisionId=@inDivisionId,
						inDepartmentId=@inDepartmentId,
						inDesignationId=@inDesignationId,
						inStatus=@inStatus,
						dtCreateDate=@getDateTimeByTimezone,
						inCreatedBy=@inCreatedBy					 
				  WHERE inDeskid=@inDeskId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH