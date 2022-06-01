-- =============================================  
-- Author:   Qutub Haider  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveStore]  
(  
	 @inStoreId INT=NULL,  
	 @stStoreName nvarchar(200),
	 @inZoneId INT,
	 @inDivisionId INT,
	 @inDepartmentId INT,
	 @inCreatedBy INT,  
	 @inSuccess INT OUT
)  
AS  
BEGIN TRY  
SET NOCOUNT ON;     
 DECLARE @getDateTimeByTimezone DATETIME =getDate()  
 SET @inSuccess=0 
	BEGIN TRAN  
	
		 IF(ISNULL(@inStoreId,0)=0)  
		 BEGIN 			
				INSERT INTO tblStore(stStoreName,inZoneId,inDivisionId,inDepartmentId, dtCreateDate,inCreatedBy)  
				SELECT  @stStoreName, @inZoneId,@inDivisionId,@inDepartmentId, @getDateTimeByTimezone, @inCreatedBy  
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE tblStore WITH(ROWLOCK) SET   
						stStoreName=@stStoreName,
						inZoneId=@inZoneId,
						inDivisionId=@inDivisionId,
						inDepartmentId=@inDepartmentId,
						dtCreateDate=@getDateTimeByTimezone,
						inCreatedBy=@inCreatedBy					 
				  WHERE inStoreId=@inStoreId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH