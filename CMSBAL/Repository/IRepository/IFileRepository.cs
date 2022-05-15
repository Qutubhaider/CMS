using CMSBAL.FIle.Models;
using CMSBAL.Trace.Models;
using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IFileRepository
    {
        void SaveFile(FileDetail foFileDetail, int fiUserId, out int fiSuccess);
        FileDetail GetFileDetail(Guid unFileId);
        List<FileListResult> GetFileList(string fsFileName, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize,int? fiUserId = null);
        public List<TraceFileResults> GetTraceFileList(int? fiDivisionId, int? fiStoreId, int? fiSRId, string fsEmployeeNo, string fsPPONo, string fsPFNo, string fsMobile, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize);
        List<Select2> GetFileDropDown();
        StoreFileDetailDropDownResult GetFileDetailDropDown(int fiFileId);



    }
}
