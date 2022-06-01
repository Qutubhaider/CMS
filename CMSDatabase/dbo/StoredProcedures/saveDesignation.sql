-- =============================================  
-- Author:   Qutub Haider  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveDesignation]  
(  
	 @inDesignationId INT=NULL,  
	 @stDesignationName nvarchar(200),
	 @inDivisionId INT,
	 @inDepartmentId INT,
	 @inZoneId INT,
	 @inCreatedBy INT,  
	 @inSuccess INT OUT
)  
AS  
BEGIN TRY  
SET NOCOUNT ON;     
 DECLARE @getDateTimeByTimezone DATETIME =getDate()  
 SET @inSuccess=0 
	BEGIN TRAN  
	
		 IF(ISNULL(@inDesignationId,0)=0)  
		 BEGIN 			
				INSERT INTO tblDesignation(stDesignationName,inDepartmentId,inDivisionId,inZoneId,dtCreateDate,inCreatedBy)  
				SELECT  @stDesignationName,@inDepartmentId,@inDivisionId,@inZoneId, @getDateTimeByTimezone, @inCreatedBy  
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE tblDesignation WITH(ROWLOCK) SET   
						stDesignationName=@stDesignationName,
						inDepartmentId = @inDepartmentId,
						inDivisionId= @inDivisionId,
						inZoneId = @inZoneId,
						dtCreateDate=@getDateTimeByTimezone,
						inCreatedBy=@inCreatedBy					 
				  WHERE inDesignationId=@inDesignationId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH