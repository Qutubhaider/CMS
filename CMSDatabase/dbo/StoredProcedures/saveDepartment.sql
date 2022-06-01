-- =============================================  
-- Author:   Qutub Haider  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveDepartment]  
(  
	 @inDepartmentId INT=NULL,  
	 @stDepartmentName nvarchar(200),
	 @inCreatedBy INT,  
	 @inSuccess INT OUT
)  
AS  
BEGIN TRY  
SET NOCOUNT ON;     
 DECLARE @getDateTimeByTimezone DATETIME =getDate()  
 SET @inSuccess=0 
	BEGIN TRAN  
	
		 IF(ISNULL(@inDepartmentId,0)=0)  
		 BEGIN 			
				INSERT INTO tblDepartment(stDepartmentName,inZoneId,inDivisionId,dtCreateDate,inCreatedBy)  
				SELECT  @stDepartmentName,0,0, @getDateTimeByTimezone, @inCreatedBy  
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE tblDepartment WITH(ROWLOCK) SET   
						stDepartmentName=@stDepartmentName,
						dtCreateDate=@getDateTimeByTimezone,
						inCreatedBy=@inCreatedBy					 
				  WHERE inDepartmentId=@inDepartmentId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH