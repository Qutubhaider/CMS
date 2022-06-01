-- ============================================= 
-- Author:  Qutub Haider 
-- EXEC [getCaseDetail]
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 

CREATE PROCEDURE getCaseDetail(
@unCaseId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inCaseId,unCaseId,SF.stFileName,SF.stEmployeeName,SF.stEmployeeNumber,SF.stPFNumber,SF.stPPONumber,SF.inEmployeeType,SF.stMobile,C.inStatus,C.inStoreFileDetailId,C.inSRId FROM tblCase C
  JOIN tblStoreFileDetails SF ON SF.inStoreFileDetailsId= C.inStoreFileDetailId
  WHERE unCaseId=@unCaseId
END