-- =============================================  
-- Author:   Qutub Haider  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  
CREATE PROC [dbo].[saveCategory]  
(  
	 @unCategoryId uniqueidentifier=null,  
	 @inCategoryId INT=NULL,
	 @inDepartmentId INT,
	 @inStatus INT,
	 @inParentCategoryId INT,
	 @stCategoryName NVARCHAR(200),
	 @inCreatedBy INT,  
	 @inSuccess INT OUT
)  
AS  
BEGIN TRY  
SET NOCOUNT ON;     
 SET @inSuccess=0 
	BEGIN TRAN  
	
		 IF(ISNULL(@inCategoryId,0)=0)  
		 BEGIN 			
				INSERT INTO [esuvidhahwh].[tblCategoryMaster](inStatus,inDepartmentId,inParentCategoryId,stCategoryName,inCreatedBy,dtCreatedDate)  
				SELECT  @inStatus, @inDepartmentId, @inParentCategoryId, @stCategoryName, @inCreatedBy , GETDATE() 
				SET @inSuccess=101  
		 END
		 ELSE  
		 BEGIN  
				  UPDATE [esuvidhahwh].[tblCategoryMaster] WITH(ROWLOCK) SET   
						inStatus=@inStatus,
						inDepartmentId=@inDepartmentId,
						inParentCategoryId=@inParentCategoryId,
						stCategoryName=@stCategoryName,
						inCreatedBy=@inCreatedBy					 
				  WHERE inCategoryId=@inCategoryId
				  SET @inSuccess=102   		   
			 END  		
	COMMIT TRAN;  
END TRY  
BEGIN CATCH  
 set @inSuccess=0  
 ROLLBACK TRAN; 
END CATCH