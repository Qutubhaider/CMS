-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 11-MAR-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/
CREATE PROCEDURE getFileDetailFromDropDown(
	@inStoreFileId INT
)
AS
BEGIN
  SELECT 
		inStoreFileDetailsId,
		stFileName,
		stEmployeeName,
		stEmployeeNumber,
		stPFNumber,
		stMobile,
		stPPONumber,
		S.stStoreName,
		R.stRoomNumber,
		A.stAlmirahNumber,
		SL.stShelveNumber
  FROM tblStoreFileDetails SD
  JOIN tblStore S ON S.inStoreId=SD.inStoreId
  JOIN tblRoom R ON R.inRoomId=SD.inRoomId
  JOIN tblAlmirah A ON A.inAlmirahId=SD.inAlmirahId
  JOIN tblShelve SL ON SL.inShelveId=SD.inShelvesId
  WHERE inStoreFileDetailsId = @inStoreFileId
END
