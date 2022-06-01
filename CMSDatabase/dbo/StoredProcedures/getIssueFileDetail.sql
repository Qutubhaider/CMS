-- =============================================  
-- Author:   Vaibhav Singh 
-- Create Date: 6-mar-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  


CREATE PROCEDURE getIssueFileDetail(
@unIssueFileDetail UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT IFH.inlssueFileId,IFH.unlssueFileId,IFH.inAssignUserId,IFH.inDivisionId,IFH.inDepartmentId,IFH.inStoreFileDetailsId,IFH.dtIssueDate,IFH.stComment,IFH.inStatus,SFD.stUnFileName,SFD.stFileName, IFH.inSRId
  FROM tblIssueFileHistory IFH
  JOIN tblStoreFileDetails SFD ON SFD.inStoreFileDetailsId=IFH.inStoreFileDetailsId

  WHERE unlssueFileId=@unIssueFileDetail
END