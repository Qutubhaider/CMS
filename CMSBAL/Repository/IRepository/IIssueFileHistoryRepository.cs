using CMSBAL.IssueFIleHistory;
using CMSBAL.IssueFIleHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
   public  interface IIssueFileHistoryRepository
    {
        void SaveIssueFile(IssueFile foIssueFile, int fiUserId, out int fiSuccess);
        void SaveIssueFileByStore(IssueFile foIssueFile, int fiUserId, out int fiSuccess);
        IssueFile GetIssueFileDetail(Guid fuIssueFileId);
        GetAssignFileDetailResult AssignFileDetailResult(Guid? fuAssignFileId);
        List<IssueFileListResult> GetIssueFileList(string fsAlmirahNumber, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null, int? fiDepartmentId = null, int? fiDivisionId = null);
        List<IssueFileListResult> GetIssueFileListByStore(string fsAlmirahNumber, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null, int? fiDepartmentId = null, int? fiDivisionId = null);
        List<IssueFileListResult> GetIssueFileListBySR(int fiSRId, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize);
        public List<IssueFileListResult> GetIssueFileListByUser(string fsFileName, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null, int? fiDepartmentId = null, int? fiDivisionId = null);
        List<IssueFileListResult> GetFileHistoryList(int fiSRId);
    }
}
