
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
  LEFT JOIN tblRoom R ON R.inRoomId=SD.inRoomId
  LEFT JOIN tblAlmirah A ON A.inAlmirahId=SD.inAlmirahId
  LEFT JOIN tblShelve SL ON SL.inShelveId=SD.inShelvesId
  WHERE inStoreFileDetailsId =@inStoreFileId
END
