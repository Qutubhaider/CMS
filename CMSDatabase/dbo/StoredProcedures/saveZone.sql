-- =============================================  
-- Author:   Qutub Haider  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveZone]  
(  
	 @inZoneId INT=NULL,  
	 @stZoneName nvarchar(200),
	 @inCreatedBy INT,  
	 @inSuccess INT OUT
)  
AS  
BEGIN TRY  
SET NOCOUNT ON;     
 DECLARE @getDateTimeByTimezone DATETIME =getDate()  
 SET @inSuccess=0 
	BEGIN TRAN  
	
		 IF(ISNULL(@inZoneId,0)=0)  
		 BEGIN 			
				INSERT INTO tblZone(stZoneName,dtCreateDate,inCreatedBy)  
				SELECT  @stZoneName, @getDateTimeByTimezone, @inCreatedBy  
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE tblZone WITH(ROWLOCK) SET   
						stZoneName=@stZoneName,
						dtCreateDate=@getDateTimeByTimezone,
						inCreatedBy=@inCreatedBy					 
				  WHERE inZoneId=@inZoneId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH