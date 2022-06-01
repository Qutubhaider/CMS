-- =============================================  
-- Author:   Qutub Haider  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveDivision]  
(  
	 @inDivisionId INT=NULL,  
	 @stDivisionName nvarchar(200),
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
	
		 IF(ISNULL(@inDivisionId,0)=0)  
		 BEGIN 			
				INSERT INTO tblDivision(stDivisionName,inZoneId,dtCreateDate,inCreatedBy)  
				SELECT  @stDivisionName, @inZoneId, @getDateTimeByTimezone, @inCreatedBy  
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE tblDivision WITH(ROWLOCK) SET   
						stDivisionName=@stDivisionName,
						inZoneId=@inZoneId,
						dtCreateDate=@getDateTimeByTimezone,
						inCreatedBy=@inCreatedBy					 
				  WHERE inDivisionId=@inDivisionId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH